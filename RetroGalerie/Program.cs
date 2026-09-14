using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using RetroGalerie.Data;
using RetroGalerie.IA.Dtos;
using RetroGalerie.IA.EF;
using RetroGalerie.IA.Interfaces;
using RetroGalerie.IA.Ollama;
using RetroGalerie.IA.Services;
using RetroGalerie.Models;
using RetroGalerie.Models.Mapping;
using RetroGalerie.Models.Mapping.Interface;
using RetroGalerie.Services;
using System.Globalization;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSingleton<SharedLocalizationService>();

builder.Services.AddIdentity<Gamer, IdentityRole<int>>(options =>
{
    options.User.RequireUniqueEmail = false;
})
   .AddDefaultUI()
   .AddEntityFrameworkStores<ApplicationDbContext>()
   .AddDefaultTokenProviders();

// Localisation avec RESX
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

// Mappers
builder.Services.AddScoped<IMapper<Game, GameViewModel>, GameMapper>();
builder.Services.AddScoped<IMapper<RetroGalerie.Data.Console, ConsoleViewModel>, ConsoleMapper>();
builder.Services.AddScoped<OllamaClient>();
builder.Services.AddScoped<IRetrievalService, RetrievalService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IGameRepository, GameRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Cultures supportées
var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("fr")
};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("fr"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
localizationOptions.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());

app.UseRequestLocalization(localizationOptions);

// Activer les WebSockets
app.UseWebSockets();

// Endpoint WebSocket IA
app.Map("/ws/chat", async context =>
{
    if (!context.WebSockets.IsWebSocketRequest)
    {
        context.Response.StatusCode = 400;
        return;
    }

    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
    var chatService = context.RequestServices.GetRequiredService<IChatService>();

    var buffer = new byte[1024 * 4];

    while (webSocket.State == WebSocketState.Open)
    {
        WebSocketReceiveResult result;

        try
        {
            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }
        catch (WebSocketException)
        {
            // Le client a quitté la page → la socket est déjà fermée/aborted
            break;
        }

        if (result.MessageType == WebSocketMessageType.Close)
        {
            if (webSocket.State == WebSocketState.Open ||
                webSocket.State == WebSocketState.CloseReceived)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
            break;
        }

        using var ms = new MemoryStream();
        ms.Write(buffer, 0, result.Count);

        while (!result.EndOfMessage)
        {
            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            ms.Write(buffer, 0, result.Count);
        }

        var question = Encoding.UTF8.GetString(ms.ToArray());

        var response = await chatService.ProcessAsync(new ChatRequest(question));
        var answerBytes = Encoding.UTF8.GetBytes(response.Answer);

        await webSocket.SendAsync(
            new ArraySegment<byte>(answerBytes),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None
        );
    }
});

// Routes MVC + Razor
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

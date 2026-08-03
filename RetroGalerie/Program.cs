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

// 👉 Service IA (ton ChatService)
builder.Services.AddScoped<IChatService, ChatService>();

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

// Ajout du CookieRequestCultureProvider pour gérer le sélecteur
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("fr"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
localizationOptions.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());

app.UseRequestLocalization(localizationOptions);

// 👉 Activer les WebSockets AVANT de les mapper
app.UseWebSockets();

// 👉 Endpoint WebSocket IA
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

    while (webSocket.State == System.Net.WebSockets.WebSocketState.Open)
    {
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        if (result.MessageType == System.Net.WebSockets.WebSocketMessageType.Close)
        {
            await webSocket.CloseAsync(
                System.Net.WebSockets.WebSocketCloseStatus.NormalClosure,
                "Closing",
                CancellationToken.None
            );
            break;
        }

        var question = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);

        var response = await chatService.ProcessAsync(new ChatRequest(question));

        var answerBytes = System.Text.Encoding.UTF8.GetBytes(response.Answer);

        await webSocket.SendAsync(
            new ArraySegment<byte>(answerBytes),
            System.Net.WebSockets.WebSocketMessageType.Text,
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

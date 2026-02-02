using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RetroGalerie.Data;
using RetroGalerie.Models.Mapping;
using RetroGalerie.Models;
using RetroGalerie.Models.Mapping.Interface;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using RetroGalerie.Services;

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

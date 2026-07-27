using Microsoft.EntityFrameworkCore;
using RetroGalerie.AI.Api.DI;
using RetroGalerie.Data;
using RetroGalerieIA.Api.Enpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DI modules
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Map endpoints
app.MapChatEndpoints();
app.MapGameSearchEndpoints();

app.Run();

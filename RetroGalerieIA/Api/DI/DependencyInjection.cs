using RetroGalerieIA.Application.Interfaces;
using RetroGalerieIA.Application.Services;
using RetroGalerieIA.Infrastructure.EF;
using RetroGalerieIA.Infrastructure.LLM;

namespace RetroGalerie.AI.Api.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IRetrievalService, RetrievalService>();
        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddSingleton<OllamaClient>();
        return services;
    }
}
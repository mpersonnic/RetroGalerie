using RetroGalerieIA.Application.Interfaces;

namespace RetroGalerieIA.Api.Enpoints
{
    public static class GameSearchEndpoints
    {
        public static void MapGameSearchEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/ai/games/search", async (string q, IRetrievalService retrieval) =>
            {
                var results = await retrieval.SearchGamesAsync(q);
                return Results.Ok(results);
            });
        }
    }
}

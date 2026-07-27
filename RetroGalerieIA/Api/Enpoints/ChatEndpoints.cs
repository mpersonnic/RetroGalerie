using RetroGalerieIA.Application.Interfaces;
using RetroGalerieIA.Domain.DTOs;

namespace RetroGalerieIA.Api.Enpoints
{
    public static class ChatEndpoints
    {
        public static void MapChatEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/ai/chat", async (ChatRequest request, IChatService chatService) =>
            {
                var response = await chatService.ProcessAsync(request);
                return Results.Ok(response);
            });
        }
    }
}

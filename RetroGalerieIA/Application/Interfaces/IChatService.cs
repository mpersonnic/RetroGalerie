using RetroGalerieIA.Domain.DTOs;

namespace RetroGalerieIA.Application.Interfaces
{
    public interface IChatService
    {
        Task<ChatResponse> ProcessAsync(ChatRequest request);
    }
}



using RetroGalerie.IA.Dtos;

namespace RetroGalerie.IA.Interfaces
{
    public interface IChatService
    {
        Task<ChatResponse> ProcessAsync(ChatRequest request);
    }
}

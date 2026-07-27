using RetroGalerieIA.Application.Interfaces;
using RetroGalerieIA.Domain.DTOs;
using RetroGalerieIA.Infrastructure.LLM;

namespace RetroGalerieIA.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IRetrievalService _retrieval;
        private readonly OllamaClient _ollama;

        public ChatService(IRetrievalService retrieval, OllamaClient ollama)
        {
            _retrieval = retrieval;
            _ollama = ollama;
        }

        public async Task<ChatResponse> ProcessAsync(ChatRequest request)
        {
            // 1. Analyse de la question
            var games = await _retrieval.SearchGamesAsync(request.Message);


            // 2. Stub IA (Ollama non installé)
            var answer = $"IA non installée. Jeux trouvés : {string.Join(", ", games.Select(g => g.Name))}";
/*            // 2. Reformulation via IA
            var answer = await _ollama.GenerateAsync(request.Message, games);
*/
            return new ChatResponse(answer);
        }
    }
}

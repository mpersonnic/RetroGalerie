using RetroGalerieIA.Application.Intents;
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
            // 1. Construire le dictionnaire dynamique
            var dict = await _retrieval.BuildIntentDictionaryAsync();

            // 2. Extraire l’intention
            var extractor = new IntentExtractor();
            var intent = extractor.Extract(request.Message, dict);

            // 3. Recherche basée sur l’intention
            var games = await _retrieval.SearchGamesAsync(intent.Subject, intent.Filters);

            // 4. IA basée sur les jeux trouvés
            var answer = await _ollama.GenerateAsync(request.Message, games);

            return new ChatResponse(answer);
        }


    }
}

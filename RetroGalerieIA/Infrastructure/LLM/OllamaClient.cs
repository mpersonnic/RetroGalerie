using RetroGalerieIA.Domain.DTOs;
using System.Text.Json;

namespace RetroGalerieIA.Infrastructure.LLM
{
    public class OllamaClient
    {
        private readonly HttpClient _http = new();

        public async Task<string> GenerateAsync(string question, IEnumerable<GameDto> games)
        {
            var context = JsonSerializer.Serialize(games);

            var payload = new
            {
                model = "llama3",
                prompt = $"Question: {question}\n\nDonnées: {context}\n\nRéponds clairement."
            };

            var response = await _http.PostAsJsonAsync("http://localhost:11434/api/generate", payload);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
    }
}

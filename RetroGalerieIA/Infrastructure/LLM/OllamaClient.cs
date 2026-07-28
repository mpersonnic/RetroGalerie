using RetroGalerieIA.Domain.DTOs;
using System.Text.Json;

namespace RetroGalerieIA.Infrastructure.LLM
{
    public class OllamaClient
    {
        private readonly HttpClient _http = new();

        public async Task<string> GenerateAsync(string question, IEnumerable<GameDto> games)
        {
            var titles = games.Select(g => g.Name).ToList();
            var list = string.Join(", ", titles);

            /*            var payload = new
                        {
                            model = "phi3",
                            stream = false,
                            prompt =
                                $"Tu es une IA experte en jeux rétro. Réponds uniquement en français.\n\n" +
                                $"Voici la liste des jeux trouvés : {list}\n\n" +
                                $"Règles :\n" +
                                $"- Ne jamais inventer de jeux.\n" +
                                $"- Ne jamais modifier les titres.\n" +
                                $"- Ne répondre qu'à partir des jeux fournis.\n" +
                                $"- Si la liste est vide, dire explicitement qu'aucun jeu n'a été trouvé.\n\n" +
                                $"Question : {question}\n\n" +
                                $"Réponds dans ce format :\n" +
                                $"jeux: [liste exacte]\n" +
                                $"commentaire: texte court"
                        };*/
            var payload = new
            {
                model = "phi3",
                stream = false,
                prompt =
                    "Tu es RetroGalerieAI, un assistant strict et factuel spécialisé dans l’inventaire de jeux rétro.\n\n" +

                    "Règles générales :\n" +
                    "- Réponds uniquement en français.\n" +
                    "- Ne fais aucune blague.\n" +
                    "- Ne fais aucun commentaire humoristique.\n" +
                    "- Ne fais aucune interprétation culturelle.\n" +
                    "- Ne fais aucune invention.\n" +
                    "- Ne fais aucune opinion.\n" +
                    "- Ne fais aucune phrase subjective.\n" +
                    "- Ne fais aucune phrase créative.\n" +
                    "- Ne fais aucune phrase hors sujet.\n" +
                    "- Ne fais aucune mention régionale ou culturelle.\n" +
                    "- Ne fais aucune comparaison.\n" +
                    "- Ne reformule pas inutilement.\n" +
                    "- Ne parle jamais de toi.\n\n" +

                    "Règles sur les données :\n" +
                    "- Réponds uniquement à partir des jeux fournis dans la liste ci‑dessous.\n" +
                    "- Ne complète jamais la liste.\n" +
                    "- Ne modifie jamais les titres.\n" +
                    "- Ne déduis jamais de jeux absents.\n" +
                    "- Si la liste est vide, réponds : \"Aucun jeu trouvé.\".\n\n" +

                    $"Liste des jeux trouvés :\n{list}\n\n" +

                    $"Question : {question}\n\n" +

                    "Format de réponse strict :\n" +
                    "jeux: [liste exacte des jeux trouvés, un par ligne]\n" +
                    "commentaire: texte court, strictement factuel, sans humour, sans interprétation\n"
            };

            var response = await _http.PostAsJsonAsync("http://localhost:11434/api/generate", payload);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var text = doc.RootElement.GetProperty("response").GetString();

            return text ?? "";
        }


    }
}

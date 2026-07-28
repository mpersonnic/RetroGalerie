using RetroGalerieIA.Application.Intents;
using RetroGalerieIA.Application.Interfaces;
using RetroGalerieIA.Domain.DTOs;

public class RetrievalService : IRetrievalService
{
    private readonly IGameRepository _repo;

    public RetrievalService(IGameRepository repo)
    {
        _repo = repo;
    }

    // Pour ton endpoint existant
    public async Task<IEnumerable<GameDto>> SearchGamesAsync(string query)
    {
        var games = await _repo.SearchAsync(query);

        return games.Select(g => new GameDto(
            g.Id,
            g.Title,
            g.Console.Name,
            g.Region
        ));
    }

    // Pour l’IA
    public async Task<IEnumerable<GameDto>> SearchGamesAsync(string subject, IEnumerable<string> filters)
    {
        var games = await _repo.SearchAsync(subject, filters);

        return games.Select(g => new GameDto(
            g.Id,
            g.Title,
            g.Console.Name,
            g.Region
        ));
    }

    // Pour l’intent detection
    public async Task<IntentDictionary> BuildIntentDictionaryAsync()
    {
        var games = await _repo.GetAllAsync();

        var dict = new IntentDictionary
        {
            GameNames = games
                .Select(g => g.Title.ToLower())
                .Distinct()
                .ToList(),

            Platforms = games
                .Select(g => g.Console.Name.ToLower())
                .Distinct()
                .ToList(),

            Genres = games
                .Select(g => g.Genre.ToLower())
                .Distinct()
                .ToList(),

            Attributes = new List<string>
        {
            "complet", "manuel", "notice", "boîte",
            "fra", "loose", "cib", "pal", "ntsc", "jap"
        },

            GameKeywords = new Dictionary<string, List<string>>()
        };

        foreach (var g in games)
        {
            var keywords = g.Title
                .ToLower()
                .Replace(":", "")
                .Replace("-", "")
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Where(w => w.Length > 2) // éviter "of", "the", etc.
                .Distinct()
                .ToList();

            dict.GameKeywords[g.Title.ToLower()] = keywords;
        }

        return dict;
    }

}

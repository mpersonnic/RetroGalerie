using RetroGalerie.IA.Dtos;
using RetroGalerie.IA.Intent;

namespace RetroGalerie.IA.Interfaces
{
    public interface IRetrievalService
    {
        Task<IEnumerable<GameDto>> SearchGamesAsync(string subject);

        Task<IEnumerable<GameDto>> SearchGamesAsync(string subject, IEnumerable<string> filters);
        Task<IntentDictionary> BuildIntentDictionaryAsync();
    }
}

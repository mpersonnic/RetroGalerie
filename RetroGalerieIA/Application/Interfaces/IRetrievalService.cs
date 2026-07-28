using RetroGalerieIA.Application.Intents;
using RetroGalerieIA.Domain.DTOs;

namespace RetroGalerieIA.Application.Interfaces
{
    public interface IRetrievalService
    {
        Task<IEnumerable<GameDto>> SearchGamesAsync(string subject);

        Task<IEnumerable<GameDto>> SearchGamesAsync(string subject, IEnumerable<string> filters);
        Task<IntentDictionary> BuildIntentDictionaryAsync();
    }
}

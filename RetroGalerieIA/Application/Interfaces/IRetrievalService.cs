using RetroGalerieIA.Domain.DTOs;

namespace RetroGalerieIA.Application.Interfaces
{
    public interface IRetrievalService
    {
        Task<IEnumerable<GameDto>> SearchGamesAsync(string query);
    }
}

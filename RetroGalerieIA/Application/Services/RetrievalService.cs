using RetroGalerieIA.Application.Interfaces;
using RetroGalerieIA.Domain.DTOs;

namespace RetroGalerieIA.Application.Services
{
    public class RetrievalService : IRetrievalService
    {
        private readonly IGameRepository _repo;

        public RetrievalService(IGameRepository repo)
        {
            _repo = repo;
        }

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
    }
}

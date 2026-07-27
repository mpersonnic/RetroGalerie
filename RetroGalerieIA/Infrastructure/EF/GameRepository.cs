using Microsoft.EntityFrameworkCore;
using RetroGalerie.Data;
using RetroGalerieIA.Application.Interfaces;

namespace RetroGalerieIA.Infrastructure.EF
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _db;

        public GameRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Game>> SearchAsync(string query)
        {
            return await _db.Games
                .Include(g => g.Console)
                .Where(g => g.Title.Contains(query))
                .ToListAsync();
        }
    }
}

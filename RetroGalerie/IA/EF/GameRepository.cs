using Microsoft.EntityFrameworkCore;
using RetroGalerie.Data;
using RetroGalerie.IA.Interfaces;


namespace RetroGalerie.IA.EF
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _db;

        public GameRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _db.Games
                .Include(g => g.Console)
                .ToListAsync();
        }

        public async Task<IEnumerable<Game>> SearchAsync(string query)
        {
            var q = query.ToLower();

            return await _db.Games
                .Include(g => g.Console)
                .Where(g =>
                    g.Title.ToLower().Contains(q) ||
                    g.Console.Name.ToLower().Contains(q) ||
                    g.Genre.ToLower().Contains(q) ||
                    g.Region.ToLower().Contains(q)
                )
                .ToListAsync();
        }

        public async Task<IEnumerable<Game>> SearchAsync(string subject, IEnumerable<string> filters)
        {
            var query = _db.Games
                .Include(g => g.Console)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(subject))
            {
                var s = subject.ToLower();
                query = query.Where(g =>
                    g.Title.ToLower().Contains(s) ||
                    g.Console.Name.ToLower().Contains(s) ||
                    g.Genre.ToLower().Contains(s));
            }

            var f = filters.Select(x => x.ToLower()).ToList();

/*            if (f.Contains("complet"))
                query = query.Where(g => g.IsComplete);

            if (f.Contains("manuel") || f.Contains("notice"))
                query = query.Where(g => g.HasManual);

            if (f.Contains("boîte"))
                query = query.Where(g => g.HasBox);*/

            if (f.Contains("fra"))
                query = query.Where(g => g.Region.ToLower().Contains("fra"));

            if (f.Contains("pal"))
                query = query.Where(g => g.Region.ToLower().Contains("pal"));

            if (f.Contains("ntsc"))
                query = query.Where(g => g.Region.ToLower().Contains("ntsc"));

            if (f.Contains("jap"))
                query = query.Where(g => g.Region.ToLower().Contains("jap"));

            return await query.ToListAsync();
        }
    }
}

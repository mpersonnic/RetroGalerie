using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetroGalerie.Data;
using RetroGalerie.Models;
using RetroGalerie.Models.Mapping.Interface;
namespace RetroGalerie.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper<Game, GameViewModel> _mapper;        
        private readonly UserManager<Gamer> _userManager;

        public GamesController(ApplicationDbContext context, IMapper<Game, GameViewModel> mapper, UserManager<Gamer> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: Games
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParam"] = sortOrder == "Title" ? "title_desc" : "Title";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["ConsoleSortParam"] = sortOrder == "Console" ? "console_desc" : "Console";

            var query = _context.Games.Include(g => g.Console).AsQueryable();

            switch (sortOrder)
            {
                case "title_desc":
                    query = query.OrderByDescending(g => g.Title);
                    break;
                case "Title":
                    query = query.OrderBy(g => g.Title);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(g => g.YearOfPublication);
                    break;
                case "Date":
                    query = query.OrderBy(g => g.YearOfPublication);
                    break;
                case "console_desc":
                    query = query.OrderByDescending(g => g.Console.Name);
                    break;
                case "Console":
                    query = query.OrderBy(g => g.Console.Name);
                    break;
                default:
                    query = query.OrderBy(g => g.Title);
                    break;
            }

            var vmList = await query.Select(g => _mapper.ToViewModel(g)).ToListAsync();
            return View(vmList);
        }



        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games
                .Include(g => g.Console)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null) return NotFound();

            return View(_mapper.ToViewModel(game));
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["ConsoleId"] = new SelectList(_context.Consoles, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameViewModel gameViewModel)
        {
            if (ModelState.IsValid)
            {
                // Vérifier si le jeu existe déjà
                bool exists = await _context.Games.AnyAsync(g =>
                    g.Title == gameViewModel.Title &&
                    g.ConsoleId == gameViewModel.ConsoleId &&
                    g.Region == gameViewModel.Region);

                if (exists)
                {
                    ModelState.AddModelError("", "Ce jeu existe déjà dans la base.");
                    ViewData["ConsoleId"] = new SelectList(_context.Consoles, "Id", "Name", gameViewModel.ConsoleId);
                    return View(gameViewModel);
                }

                var fileName = "default.png";
                if (gameViewModel.CoverImageFile != null)
                {
                    fileName = Path.GetFileName(gameViewModel.CoverImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/images/covers", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await gameViewModel.CoverImageFile.CopyToAsync(stream);
                    }
                }
                var game = _mapper.ToEntity(gameViewModel);
                game.CoverImageUrl = "/images/covers/" + fileName;
                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                // Associer au joueur connecté
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    _context.GameGamers.Add(new GameGamer
                    {
                        GameId = game.Id,
                        UserId = user.Id,
                        Note = 0,
                        Owned = gameViewModel.Owned,
                    });
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ConsoleId"] = new SelectList(_context.Consoles, "Id", "Name", gameViewModel.ConsoleId);
            return View(gameViewModel);
        }



        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games.FindAsync(id);
            if (game == null) return NotFound();

            var vm = _mapper.ToViewModel(game);

            // 🔥 Récupérer l'utilisateur connecté
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                // 🔥 Récupérer la relation GameGamer
                var relation = await _context.GameGamers
                    .FirstOrDefaultAsync(gg => gg.UserId == user.Id && gg.GameId == id);

                if (relation != null)
                {
                    vm.Owned = relation.Owned; // Jeu possédé ou non
                }
            }

            ViewData["ConsoleId"] = new SelectList(_context.Consoles, "Id", "Name", game.ConsoleId);
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GameViewModel gameViewModel)
        {
            if (id != gameViewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var gameSaved = await _context.Games.FindAsync(id);
                if (gameSaved == null) return NotFound();

                try
                {
                    var fileName = gameSaved.CoverImageUrl; // valeur par défaut

                    if (gameViewModel.CoverImageFile != null)
                    {
                        var newFileName = Path.GetFileName(gameViewModel.CoverImageFile.FileName);
                        fileName = "/images/covers/" + newFileName;
                        var filePath = Path.Combine("wwwroot/images/covers", newFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await gameViewModel.CoverImageFile.CopyToAsync(stream);
                        }
                    }

                    // si aucune nouvelle image, on garde l’ancienne
                    gameSaved.CoverImageUrl = fileName;

                    // Mettre à jour les propriétés de l’entité existante
                    gameSaved.Title = gameViewModel.Title;
                    gameSaved.YearOfPublication = gameViewModel.YearOfPublication;
                    gameSaved.Description = gameViewModel.Description;
                    gameSaved.NumberOfPlayers = gameViewModel.NumberOfPlayers;
                    gameSaved.CoverImageUrl = fileName;
                    gameSaved.Genre = gameViewModel.Genre;
                    gameSaved.Developer = gameViewModel.Developer;
                    gameSaved.Publisher = gameViewModel.Publisher;
                    gameSaved.Region = gameViewModel.Region;
                    gameSaved.Language = gameViewModel.Language;
                    gameSaved.ConsoleId = gameViewModel.ConsoleId;

                    await _context.SaveChangesAsync(); // pas besoin de Update(), EF suit déjà l’entité
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Games.Any(e => e.Id == gameViewModel.Id))
                        return NotFound();
                    else
                        throw;
                }

                // 🔥 Mise à jour de la relation GameGamer
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var relation = await _context.GameGamers
                        .FirstOrDefaultAsync(gg => gg.UserId == user.Id && gg.GameId == id);

                    if (relation != null)
                    {
                        relation.Owned = gameViewModel.Owned;
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ConsoleId"] = new SelectList(_context.Consoles, "Id", "Name", gameViewModel.ConsoleId);
            return View(gameViewModel);
        }


        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games.FirstOrDefaultAsync(m => m.Id == id);
            if (game == null) return NotFound();

            return View(_mapper.ToViewModel(game));
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // API pour suggestions
        [HttpGet]
        public JsonResult Autocomplete(string term)
        {
            if (string.IsNullOrWhiteSpace(term) || term.Length < 3)
                return Json(new string[] { });

            var suggestions = _context.Games
                .Where(g => g.Title.Contains(term))
                .Select(g => new { g.Id, g.Title })
                .Take(5)
                .ToList();

            return Json(suggestions);
        }
    }
}
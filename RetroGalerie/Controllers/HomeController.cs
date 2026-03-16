using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using RetroGalerie.Data;
using RetroGalerie.Models;
using System.Diagnostics;

namespace RetroGalerie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Gamer> _userManager;


        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context, UserManager<Gamer> userManager)
        {
            _context = context;            
            _logger = logger;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;

            var consoles = _context.Consoles
                .Select(c => new ConsoleGroupViewModel
                {
                    ConsoleName = c.Name,
                    Games = c.Games.Select(g => new GameViewModel
                    {
                        Id = g.Id,
                        Title = g.Title,
                        CoverImageUrl = g.CoverImageUrl,
                        ConsoleName = c.Name,
                        Owned = _context.GameGamers
                            .Any(gg => gg.GameId == g.Id && gg.UserId == user.Id && gg.Owned)
                    }).ToList()
                }).ToList();


            var vm = new HomeViewModel
            {
                Consoles = consoles
            };

            return View(vm);
        }



        [Authorize]
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace RetroGalerie.Controllers
{
    public class IAController : Controller
    {
        public IActionResult Chat()
        {
            return View();
        }
    }
}

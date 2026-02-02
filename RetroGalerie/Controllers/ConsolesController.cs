using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroGalerie.Data;
using RetroGalerie.Models;
using RetroGalerie.Models.Mapping.Interface;

namespace RetroGalerie.Controllers
{
    [Authorize]
    public class ConsolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper<Data.Console, ConsoleViewModel> _mapper;

        public ConsolesController(ApplicationDbContext context, IMapper<Data.Console, ConsoleViewModel> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["CurrentSort"] = sortOrder;
            
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["ManufacturerSortParm"] = sortOrder == "manufacturer" ? "manufacturer_desc" : "manufacturer";
            ViewData["YearSortParm"] = sortOrder == "year" ? "year_desc" : "year";

            var consoles = from c in _context.Consoles select c;

            switch (sortOrder)
            {
                case "name_desc":
                    consoles = consoles.OrderByDescending(c => c.Name);
                    break;
                case "manufacturer":
                    consoles = consoles.OrderBy(c => c.Manufacturer);
                    break;
                case "manufacturer_desc":
                    consoles = consoles.OrderByDescending(c => c.Manufacturer);
                    break;
                case "year":
                    consoles = consoles.OrderBy(c => c.ReleaseYear);
                    break;
                case "year_desc":
                    consoles = consoles.OrderByDescending(c => c.ReleaseYear);
                    break;
                default:
                    consoles = consoles.OrderBy(c => c.Name);
                    break;
            }

            var vmList = consoles.Select(c => _mapper.ToViewModel(c)).ToList();
            return View(vmList);
        }



        // GET: Consoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var console = await _context.Consoles
                .Include(c => c.Games)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (console == null) return NotFound();

            return View(_mapper.ToViewModel(console));
        }

        // GET: Consoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsoleViewModel consoleViewModel)
        {
            if (ModelState.IsValid)
            {
                if (consoleViewModel.ImageFile != null && consoleViewModel.ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/consoles");
                    Directory.CreateDirectory(uploadsFolder);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(consoleViewModel.ImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await consoleViewModel.ImageFile.CopyToAsync(stream);
                    }

                    consoleViewModel.ImageUrl = "/images/consoles/" + fileName;
                }

                _context.Consoles.Add(_mapper.ToEntity(consoleViewModel));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consoleViewModel);
        }



        // GET: Consoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var console = await _context.Consoles.FindAsync(id);
            if (console == null) return NotFound();

            return View(_mapper.ToViewModel(console));
        }

        // POST: Consoles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ConsoleViewModel vm)
        {
            if (id != vm.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var console = await _context.Consoles.FindAsync(id);
                    if (console == null) return NotFound();

                    // Mise à jour des champs
                    console.Name = vm.Name;
                    console.Manufacturer = vm.Manufacturer;
                    console.ReleaseYear = vm.ReleaseYear;
                    console.Description = vm.Description;

                    if (vm.ImageFile != null && vm.ImageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/consoles");
                        Directory.CreateDirectory(uploadsFolder);

                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.ImageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await vm.ImageFile.CopyToAsync(stream);
                        }

                        console.ImageUrl = "/images/consoles/" + fileName;
                    }
                    else
                    {
                        // 👇 garder l’ancienne valeur si aucune nouvelle image n’est uploadée
                        console.ImageUrl = console.ImageUrl;
                    }

                    _context.Update(console);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Consoles.Any(e => e.Id == vm.Id))
                        return NotFound();
                    else
                        throw;
                }
            }
            return View(vm);
        }


        // GET: Consoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var console = await _context.Consoles.FirstOrDefaultAsync(m => m.Id == id);
            if (console == null) return NotFound();

            return View(_mapper.ToViewModel(console));
        }

        // POST: Consoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var console = await _context.Consoles.FindAsync(id);
            if (console != null)
            {
                _context.Consoles.Remove(console);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

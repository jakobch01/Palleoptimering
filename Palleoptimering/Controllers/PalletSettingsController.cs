using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Palleoptimering.Models.DataAccess;
using Palleoptimering.Models.Domain;

namespace Palleoptimering.Controllers
{
    public class PalletSettingsController : Controller
    {
        private readonly AppDbContext _context;

        public PalletSettingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Vis eksisterende palleindstillinger
        public async Task<IActionResult> Index()
        {
            var settings = await _context.PalletSettings.FirstOrDefaultAsync();
            return View(settings ?? new PalletSettings());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PalletSettings model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Opdatere settings hvis en findes i databasen
            var existingSettings = await _context.PalletSettings.FirstOrDefaultAsync();

            if (existingSettings != null)
            {
                model.Id = existingSettings.Id;

                _context.Entry(existingSettings).CurrentValues.SetValues(model);
            }
            else
            {
                await _context.PalletSettings.AddAsync(model);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Indstillingerne blev gemt.";
            return RedirectToAction(nameof(Index));
        }
    }
}

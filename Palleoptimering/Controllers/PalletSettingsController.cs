using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Palleoptimering.Models;

namespace Palleoptimering.Controllers
{
    public class PalletSettingsController : Controller
    {
        private readonly PalletSettingsDbContext _context;
        public PalletSettingsController(PalletSettingsDbContext context) 
        { 
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePalletSettings(PalletSettings model)
        {
            if (ModelState.IsValid) 
            {
                var existingPalletSettings = await _context.PalletSettings
                    .FirstOrDefaultAsync(p => p.Id == model.Id);
                if (existingPalletSettings != null)
                {
                    existingPalletSettings.MaxLayers = model.MaxLayers;
                    existingPalletSettings.MaxSpace = model.MaxSpace;
                    existingPalletSettings.WeightAllowedToTurnElement = model.WeightAllowedToTurnElement;
                    existingPalletSettings.HeightWidthFactor = model.HeightWidthFactor;
                    existingPalletSettings.HeightWidthFactorOnlyForOneElement = model.HeightWidthFactorOnlyForOneElement;
                    existingPalletSettings.StackingMaxHeight = model.StackingMaxHeight;
                    existingPalletSettings.EndPlate = model.EndPlate;
                    existingPalletSettings.AllowedStackingMaxWeight = model.AllowedStackingMaxWeight;
                    existingPalletSettings.AllowTurningOverMaxHeight = model.AllowTurningOverMaxHeight;
                    _context.PalletSettings.Update(existingPalletSettings);

                } else
                {
                    _context.PalletSettings.Add(model);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}

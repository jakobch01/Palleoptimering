using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Palleoptimering.Models.DataAccess;
using Palleoptimering.Models.Domain;
using Palleoptimering.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("[controller]")]
public class OrderController : Controller
{
    private readonly AppDbContext _context;
    private PalletOptimizer _optimizer;

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new OrderViewModel
        {
            Elements = SampleElements()
        });
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(OrderViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var order = new Order
        {
            Customer = model.Customer,
            Elements = model.Elements
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var pallets = await _context.Pallets.Where(p => p.IsActive).ToListAsync();
        var settings = await _context.PalletSettings.FirstOrDefaultAsync();

        if (settings == null)
        {
            TempData["Error"] = "Ingen palleindstillinger fundet.";
            return RedirectToAction("Index");
        }

        _optimizer = new PalletOptimizer(pallets, settings);
        var plan = _optimizer.Optimize(order.Elements);

        
        

        return RedirectToAction("Result", plan);
    }

    public IActionResult Result()
    {
        var result = _optimizer.Optimize(_context.Elements.ToList());
        return View(result);
    }


    [HttpPost("generate-pallet-plan")]
    public async Task<IActionResult> GeneratePalletPlan()
    {
        var orders = await _context.Orders.Include(o => o.Elements).ToListAsync();
        var pallets = await _context.Pallets.Where(p => p.IsActive).ToListAsync();
        var settings = await _context.PalletSettings.FirstOrDefaultAsync();

        if (settings == null)
        {
            TempData["Error"] = "Ingen palleindstillinger fundet.";
            return RedirectToAction("Index");
        }

        List<string> plan = new();

        foreach (var order in orders)
        {
            var remainingElements = new List<Element>(order.Elements);
            int palletCount = 0;

            

            plan.Add($"Ordre {order.Id} ({order.Customer}): {palletCount} paller brugt.");
        }

        TempData["PalletPlan"] = string.Join("\n", plan);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _context.Orders.Include(o => o.Elements).ToListAsync();
        return View(orders);
    }


    private List<Element> SampleElements() => new()
    {
            new Element { Name = "Vindue 120x120", Width = 1200, Height = 1200, Depth = 100, Weight = 25, Rotation = RotationBehavior.Allowed, RequiresSpecialPallet = false, PalletType = "Standard", MaxElementsPerPallet = 10, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch001" },
            new Element { Name = "Vindue 90x90", Width = 900, Height = 900, Depth = 100, Weight = 18, Rotation = RotationBehavior.NotAllowed, RequiresSpecialPallet = false, PalletType = "Standard", MaxElementsPerPallet = 15, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch001" },
            new Element { Name = "Vindue 150x120", Width = 1500, Height = 1200, Depth = 120, Weight = 30, Rotation = RotationBehavior.Allowed, RequiresSpecialPallet = false, PalletType = "HeavyDuty", MaxElementsPerPallet = 8, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch001" },
            new Element { Name = "Glasdør 210x90", Width = 900, Height = 2100, Depth = 80, Weight = 40, Rotation = RotationBehavior.Required, RequiresSpecialPallet = true, PalletType = "Special", MaxElementsPerPallet = 1, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch002" },
            new Element { Name = "Glasdør 200x80", Width = 800, Height = 2000, Depth = 80, Weight = 36, Rotation = RotationBehavior.Required, RequiresSpecialPallet = true, PalletType = "Special", MaxElementsPerPallet = 1, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch002" },
            new Element { Name = "Vindue 120x60", Width = 1200, Height = 600, Depth = 100, Weight = 20, Rotation = RotationBehavior.Allowed, RequiresSpecialPallet = false, PalletType = "Standard", MaxElementsPerPallet = 12, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch001" },
            new Element { Name = "Vindue 180x120", Width = 1800, Height = 1200, Depth = 120, Weight = 35, Rotation = RotationBehavior.NotAllowed, RequiresSpecialPallet = false, PalletType = "HeavyDuty", MaxElementsPerPallet = 6, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch001" },
            new Element { Name = "Fast karm vindue 100x100", Width = 1000, Height = 1000, Depth = 70, Weight = 15, Rotation = RotationBehavior.Allowed, RequiresSpecialPallet = false, PalletType = "Standard", MaxElementsPerPallet = 20, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch001" },
            new Element { Name = "Dobbelt glasdør 210x160", Width = 1600, Height = 2100, Depth = 90, Weight = 60, Rotation = RotationBehavior.Required, RequiresSpecialPallet = true, PalletType = "Special", MaxElementsPerPallet = 1, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch002" },
            new Element { Name = "Vindue 60x60", Width = 600, Height = 600, Depth = 80, Weight = 10, Rotation = RotationBehavior.Allowed, RequiresSpecialPallet = false, PalletType = "Standard", MaxElementsPerPallet = 25, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch003" },
            new Element { Name = "Box A", Width = 400, Height = 300, Depth = 500, Weight = 20.5m, Rotation = RotationBehavior.Allowed, RequiresSpecialPallet = false, PalletType = "Standard", MaxElementsPerPallet = 10, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch004" },
            new Element { Name = "Box B", Width = 350, Height = 250, Depth = 400, Weight = 15.0m, Rotation = RotationBehavior.Required, RequiresSpecialPallet = false, PalletType = "Standard", MaxElementsPerPallet = 10, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch004" },
            new Element { Name = "Box C", Width = 500, Height = 400, Depth = 600, Weight = 30.0m, Rotation = RotationBehavior.NotAllowed, RequiresSpecialPallet = true, PalletType = "Heavy", MaxElementsPerPallet = 5, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch004" },
            new Element { Name = "Box D", Width = 450, Height = 350, Depth = 550, Weight = 18.0m, Rotation = RotationBehavior.Allowed, RequiresSpecialPallet = false, PalletType = "Standard", MaxElementsPerPallet = 10, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch004" },
            new Element { Name = "Fragile Component", Width = 600, Height = 500, Depth = 700, Weight = 22.0m, Rotation = RotationBehavior.NotAllowed, RequiresSpecialPallet = true, PalletType = "Reinforced", MaxElementsPerPallet = 1, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch005" },
            new Element { Name = "Plastic Unit", Width = 300, Height = 200, Depth = 400, Weight = 10.0m, Rotation = RotationBehavior.Allowed, RequiresSpecialPallet = false, PalletType = "Standard", MaxElementsPerPallet = 10, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch006" },
            new Element { Name = "Large Box E", Width = 750, Height = 450, Depth = 1000, Weight = 35.0m, Rotation = RotationBehavior.Required, RequiresSpecialPallet = false, PalletType = "Standard", MaxElementsPerPallet = 10, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch004" },
            new Element { Name = "Medium Box F", Width = 500, Height = 300, Depth = 600, Weight = 25.0m, Rotation = RotationBehavior.Allowed, RequiresSpecialPallet = false, PalletType = "Standard", MaxElementsPerPallet = 10, OrderId = 1, OptimizationGroup = "GroupA", Series = "Batch004" },
            new Element { Name = "Element A", Width = 500, Height = 1000, Depth = 300, Weight = 5.25m, OrderId = 101, Series = "Batch001", Rotation = RotationBehavior.Allowed, RequiresSpecialPallet = false, MaxElementsPerPallet = 10, PalletType = "Wooden", IsGeometric = false, OptimizationGroup = "GroupA" },
            new Element { Name = "Element B", Width = 800, Height = 1500, Depth = 400, Weight = 7.75m, OrderId = 102, Series = "Batch002", Rotation = RotationBehavior.Required, RequiresSpecialPallet = true, MaxElementsPerPallet = 20, PalletType = "Plastic", IsGeometric = true, OptimizationGroup = "GroupA" },
            new Element { Name = "Element C", Width = 600, Height = 1200, Depth = 350, Weight = 6.50m, OrderId = 103, Series = "Batch003", Rotation = RotationBehavior.NotAllowed, RequiresSpecialPallet = false, MaxElementsPerPallet = 15, PalletType = "Metal", IsGeometric = false, OptimizationGroup = "GroupA" }
};

}

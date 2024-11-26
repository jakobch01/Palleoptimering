using Microsoft.AspNetCore.Mvc;
using Palleoptimering.Models;

namespace Palleoptimering.Controllers
{
    public class ElementController : Controller
    {
        private readonly ElementService _elementService;
        private readonly XmlElementLoader _xmlElementLoader;

        // Controllerens constructor
        public ElementController(ElementService elementService, XmlElementLoader xmlElementLoader)
        {
            _elementService = elementService;
            _xmlElementLoader = xmlElementLoader;
        }

        // Index metode som håndterer sortering af elementer
        public IActionResult Index(string sortOption)
        {
            // Brug LoadElementsFromXml-metoden uden at overføre filsti
            var elements = _xmlElementLoader.LoadElementsFromXml();

            // Sorter elementerne baseret på det angivne sortOption
            if (sortOption == "length")
            {
                elements = _elementService.SortByLength(elements);
            }
            else if (sortOption == "weight")
            {
                elements = _elementService.SortByWeight(elements);
            }

            // Returner view med de sorterede elementer
            return View(elements);
        }

        // GroupElements metode som grupperer elementerne
        public IActionResult GroupElements()
        {
            // Brug LoadElementsFromXml-metoden uden at overføre filsti
            var elements = _xmlElementLoader.LoadElementsFromXml();
            var groupedElements = _elementService.GroupElements(elements);

            // Returner grupperede elementer til view
            return View(groupedElements);
        }
    }
}

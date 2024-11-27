using Microsoft.AspNetCore.Mvc;
using Palleoptimering.Models;

namespace Palleoptimering.Controllers
{
    public class OrderController : Controller
    {
        private readonly XmlOrderLoader _xmlOrderLoader;

        // Controllerens constructor
        public OrderController(XmlOrderLoader xmlOrderLoader)
        {
            _xmlOrderLoader = xmlOrderLoader;
        }

        // Index metode som håndterer visning af ordrer
        public IActionResult Index()
        {
            // Hent ordrer fra XML
            var orders = _xmlOrderLoader.LoadOrdersFromXml();

            // Returner view med de hentede ordrer
            return View(orders);
        }
    }
}

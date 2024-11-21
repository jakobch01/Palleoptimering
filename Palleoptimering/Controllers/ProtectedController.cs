using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Palleoptimering.Controllers
{
    [Authorize]
    public class ProtectedController : Controller
    {
        public IActionResult SecureAction()
        {
            return View();
        }
    }
}

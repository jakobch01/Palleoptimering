﻿using Microsoft.AspNetCore.Mvc;

namespace Palleoptimering.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}


﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Palleoptimering.Models;
using Palleoptimering.Models.ViewModels;

namespace Palleoptimering.Controllers
{
    public class HomeController : Controller
    {

        private IPalletRepository repository;

        public HomeController(IPalletRepository repo)
        {
            repository = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ViewResult ListPallets()
        {
            return View(new PalletListViewModel { Pallets = repository.Pallets});
        }

        [HttpGet]
        public ViewResult CreatePallet()
        {
            return View();
        }

        [HttpPost]  
        public IActionResult CreatePallet(Pallet pallet) 
        {
            if (ModelState.IsValid) 
            {
                repository.AddPallet(pallet);
                return RedirectToAction("ListPallets");
            } else
            {
                return View();
            }

        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pallet = repository.Pallets
                .FirstOrDefault(m => m.Id == id);
            if (pallet == null)
            {
                return NotFound();
            }

            return View(pallet);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            var pallet = repository.Pallets.Where(p => p.Id == id).FirstOrDefault();
            repository.DeletePallet(pallet);
            return RedirectToAction("ListPallets");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var pallet = repository.Pallets.FirstOrDefault(p => p.Id == id);
            if (pallet == null)
            {
                return NotFound();
            }
            return View(pallet);
        }

        [HttpPost]
        public IActionResult Edit(Pallet pallet)
        {
            if (ModelState.IsValid)
            {
                repository.UpdatePallet(pallet);
                return RedirectToAction("ListPallets");
            }
            return View(pallet);
        }

    }
}

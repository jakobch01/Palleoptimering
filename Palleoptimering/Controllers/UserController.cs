using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Palleoptimering.Models;
using System.Threading.Tasks;

namespace Palleoptimering.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /User/Login
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl; // For tracking the URL to redirect after login
            return View();
        }

        // POST: /User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/"); // Default to home if no return URL is set

            if (ModelState.IsValid)
            {
                var signin = await _userManager.FindByNameAsync(model.Username);
                if (signin == null)
                {
                    ModelState.AddModelError(string.Empty, "Brugernavn eller adgangskode er forkert.");
                    return View(model);
                }

                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(signin, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl); // Redirect back to the original page after login
                }

                ModelState.AddModelError(string.Empty, "Brugernavn eller adgangskode er forkert.");
                return View(model);
            }

            return View(model);
        }

        // GET: /User/Create (Register page)
        public IActionResult Create()
        {
            return View();
        }

        // POST: /User/Create (Register new user)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Register model)
        {
            if (ModelState.IsValid)
            {
                // Ensure passwords match
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Adgangskoderne stemmer ikke overens.");
                    return View(model);
                }

                var user = new IdentityUser { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Automatically sign in the new user
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home"); // Redirect to the homepage after registration
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // Logout action
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); // Redirect to the home page after logout
        }

        // Helper method to handle redirection after login
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}

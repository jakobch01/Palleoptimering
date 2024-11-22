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
            // Set up the returnUrl to redirect to after login
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/"); // Default to the home page if no return URL is provided

            if (ModelState.IsValid)
            {
                // Attempt to find the user by the provided username
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    // If user not found, show error message
                    ModelState.AddModelError(string.Empty, "Brugernavn eller adgangskode er forkert.");
                    return View(model);
                }

                // Sign the user in
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Redirect to the return URL or home page after successful login
                    return RedirectToLocal(returnUrl);
                }

                // If login failed, show an error message
                ModelState.AddModelError(string.Empty, "Brugernavn eller adgangskode er forkert.");
            }

            return View(model);
        }

        // GET: /User/Create (Register page)
        public IActionResult Create()
        {
            return View();
        }

        // POST: /User/Create (Register a new user)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Register model)
        {
            if (ModelState.IsValid)
            {

                Console.WriteLine("Model er gyldig.");
                Console.WriteLine($"Brugernavn: {model.Username}, Email: {model.Email}");


                // Create a new IdentityUser
                var user = new IdentityUser { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    Console.WriteLine("Brugeren blev oprettet.");
                    // Sign in the user after successful registration
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                // Log errors from the result and display them
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Fejl: {error.Description}");
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                Console.WriteLine("ModelState er ugyldig.");
            }

            // If model validation fails or user creation fails, show the registration page again
            return View(model);
        }

        // Logout action
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); // Redirect to the home page after logging out
        }

        // Helper method to handle redirection after login
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)) // Check if the URL is local
            {
                return Redirect(returnUrl); // Redirect to the local URL
            }
            else
            {
                return RedirectToAction("Index", "Home"); // Default fallback to the home page
            }
        }
    }
}


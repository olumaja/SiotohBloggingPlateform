using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiotohBloggingPlateform.Model.Models;
using SiotohBloggingPlateform.Model.ViewModels;

namespace SiotohBloggingPlateform.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {

        private readonly UserManager<Model.Models.User> _userManager;
        private readonly SignInManager<Model.Models.User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<Model.Models.User> userManager, SignInManager<Model.Models.User> signInManager, RoleManager<IdentityRole> roleManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    ModelState.AddModelError("", "Account already exist");
                    return View(model);
                }

                var userToAdd = new Model.Models.User
                {
                    Name = model.Name,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await _userManager.CreateAsync(userToAdd, model.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                await _userManager.AddToRoleAsync(userToAdd, "User");
                await _signInManager.SignInAsync(userToAdd, isPersistent: false);
                //TempData["registerMessage"] = "Registration successful, kindly login.";
                return RedirectToAction("Index", "PrivatePage", new {area = "User"});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View(model);
            }
            
            if (returnUrl != "/") { return Redirect(returnUrl); }

            return RedirectToAction("Index", "PrivatePage", new { area = "User" });
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

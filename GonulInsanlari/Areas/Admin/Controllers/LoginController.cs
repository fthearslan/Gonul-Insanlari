using EntityLayer.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Logging;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        SignInManager<AppUser> _signInManager;
       
        private readonly ILogger<LoginController> _logger;

        public LoginController(SignInManager<AppUser> signInManager, ILogger<LoginController> Logger)
        {
            _signInManager = signInManager;
            _logger = Logger;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel user)
        {

            if (ModelState.IsValid)
            {
                var login = await _signInManager.PasswordSignInAsync(user.Username, user.Password, false, true);
                if (login.Succeeded)
                {
                    _logger.LogInformation($"The user with username {user.Username} has logged in.");
                    return RedirectToAction("Index", "Dashboard", "Admin");
                }
                else
                {
                    TempData["Error"] = "Invalid username or password, please provide valid credentials.";
                }
                return View(user);
            }
            return View(user);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


    }
}

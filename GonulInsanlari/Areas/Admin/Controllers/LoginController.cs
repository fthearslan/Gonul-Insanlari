using EntityLayer;
using GonulInsanlari.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        SignInManager<AppUser> _signInManager;
       
        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
       
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
                var login= await _signInManager.PasswordSignInAsync(user.Username, user.Password,false,true);
                if (login.Succeeded)
                {
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

       


    }
}

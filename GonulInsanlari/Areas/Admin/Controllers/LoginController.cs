using BussinessLayer.Abstract.Services;
using DataAccessLayer.Concrete.Providers;
using DataAccessLayer.Migrations;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Enums;
using GonulInsanlari.Extensions.Admin;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using ViewModelLayer.Models.Configuration;
using ViewModelLayer.Models.Newsletter;
using ViewModelLayer.ViewModels.Login;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [AllowAnonymous]
    [Route("login")]
    public class LoginController : Controller
    {
        SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("admin")]

        public IActionResult Login()
        {





            return View();

        }


        [HttpPost]
        [Route("admin")]
        public async Task<IActionResult> Login([FromServices] IEmailService mailManager, SignInViewModel user)
        {

            if (!ModelState.IsValid)
                return View(user);

            var login = await _signInManager.PasswordSignInAsync(user.Username, user.Password, false, true);

            if (login.IsNotAllowed)
            {
                if (await mailManager.SendConfirmationLinkAsync(new(user.Username, "confirm-email", HttpContext)))
                {
                    TempData["MailInfo"] = "To log in, you need to confirm your email address first, we have sent you a confirmation link.";

                    return View(user);
                }

            }

            if (login.IsLockedOut)
                return RedirectToAction(nameof(LockedOut), new { username = user.Username });

            if (login.Succeeded)
            {
                var usersignedIn = await _signInManager.UserManager.FindByNameAsync(user.Username);

                if (usersignedIn.Status is true)
                {
                    await _signInManager.LogUserLoginAsync(user.Username, LoginType.Login);

                    return RedirectToAction("Index", "Dashboard", "Admin");

                }

            }

            TempData["Error"] = "Invalid username or password, please provide valid credentials.";

            return View(user);

        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var userName = _signInManager.Context?.User?.Identity?.Name;

            await _signInManager.SignOutAsync();

            if (userName is not null)
                await _signInManager.LogUserLoginAsync(userName, LoginType.Logout);

            return RedirectToAction("Login");

        }

        public async Task<IActionResult> LockedOut(string username)
        {

            AppUser? user = await _signInManager.UserManager.FindByNameAsync(username);

            ViewData["LockOutEnd"] = user?
                .LockoutEnd
                .GetValueOrDefault().
                LocalDateTime;

            return View();

        }




    }


}

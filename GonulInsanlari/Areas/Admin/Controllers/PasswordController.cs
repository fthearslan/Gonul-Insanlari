using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ViewModelLayer.Models.Newsletter;
using ViewModelLayer.ViewModels.Login;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area(nameof(Admin))]
    [Route("password")]
    public class PasswordController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        public PasswordController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Route("forgot-password")]
        public IActionResult ForgotPassword()
        {


            return View();

        }


        [Route("forgot-password")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromServices] IEmailService _emailManager, ForgotPasswordViewModel model)
        {

            AppUser user = await _userManager.FindByEmailAsync(model.Email);


            
            if (user is null)
            {
                TempData["Error"] = "There is no user found with this email.";

                return View(model);

            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string? resetPasswordLink = Url.Action("ResetPassword", "Password", new
            {
                userId = user.Id.ToString(),
                token = token

            }, HttpContext.Request.Scheme);

            if (token is null || resetPasswordLink is null)
            {
                TempData["Error"] = "Something went wrong, please try again later.";

                return View(model);


            }

            await _emailManager.SendResetPasswordLinkAsync(new SendMailModel()
            {
                To = new() { model.Email },
                Subject = "Reset Password Request",
                Content = resetPasswordLink,
                Status = ContactStatus.Sent,
            });

            TempData["Success"] = " Reset password link has been successfully sent, please check your email.";

            return View(model);


        }

        [Route("reset-password")]
        public IActionResult ResetPassword(string userId, string token)
        {


            ResetPasswordViewModel model = new()
            {
                userId = userId,
                token = token

            };


            return View(model);


        }

        [Route("reset-password")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            if (!ModelState.IsValid) { return View(model); };


            var user = await _userManager.FindByIdAsync(model.userId.ToString());

            if (user is null)
                return RedirectToAction("Login", "Login");

            IdentityResult result = await _userManager.ResetPasswordAsync(user, model.token.ToString(), model.NewPassword);

            if (result.Succeeded)
                return RedirectToAction("Login", "Login");

            TempData["Error"] = "Something went wrong, please try again later.";

            return View(model);


        }


    }
}

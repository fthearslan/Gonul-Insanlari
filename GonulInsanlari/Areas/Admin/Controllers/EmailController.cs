using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewModelLayer.ViewModels.Email;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area(nameof(Admin))]
    [Route("mail")]
    public class EmailController : Controller
    {

        private readonly UserManager<AppUser> userManager;

        public EmailController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [Route("confirmed",Name ="confirm-email")]
        public async Task<IActionResult> EmailConfirmed(string userId, string token)
        {

            if (userId is null || token is null)
                return RedirectToRoute("Login");


            AppUser user = await userManager.FindByIdAsync(userId);

            if (user is null)
                return RedirectToRoute("Login");


            if (user.EmailConfirmed)
                return RedirectToRoute("Login");


            user.EmailConfirmed = true;

            IdentityResult result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                ViewData["Success"] = " Email has been successfully confirmed. You can go back to login page to log in.";
            }
            else
            {
                ViewData["Error"] = "Something went wrong while confirming your email, please try again later.";

            }

            return View();
        }

        [Route("confirm",Name ="confirm-email-on-register")]
        public IActionResult ConfirmEmailOnRegister(string userId, string token)
        {
            if (userId is null || token is null)
                return BadRequest();

            ConfirmEmailViewModel model = new ConfirmEmailViewModel(userId, token);

            return View(model);

        }

        [HttpPost]
        [Route("confirm")]
        public async Task<IActionResult> ConfirmEmailOnRegister(ConfirmEmailViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            AppUser user = await userManager.FindByIdAsync(model.UserId);

            if (user is null)
                return BadRequest();

            user.EmailConfirmed = true;

            IdentityResult result = await userManager.AddPasswordAsync(user, model.Password);

            if (result.Succeeded)
                return RedirectToRoute("Login");


            List<string> errors = new();

            foreach (var item in result.Errors)
                errors.Add(item.Description);

            ViewData["Errors"] = errors;

            return View(model);

        }

        [Route("confirmed-subscription",Name ="confirm-on-subscribe")]
        public IActionResult ConfirmEmailOnSubscription([FromServices] INewsLetterService _newsLetterManager, string email)
        {

            NewsLetter? newsLetterSubscriber = _newsLetterManager
                   .GetWhere(x => x.MailAddress == email)
                   .First();

            if (newsLetterSubscriber is null)
            {
                @ViewData["Error"] = "Something went wrong while executing the request, please try again later and if it continues, please contact with support.";
                return View();

            }

            newsLetterSubscriber.SubscriberStatus = SubscriberStatus.Active;
            newsLetterSubscriber.EmailConfirmed = true;
            newsLetterSubscriber.SubscriptionStartDate = DateTime.Now;

            _newsLetterManager.Update(newsLetterSubscriber);

            @ViewData["Success"] = "Email has been successfully confirmed. You are subscribing our newsletter from now on.";

            return View();
        }
    }
}

using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModelLayer.ViewModels.Newsletter;

namespace GonulInsanlari.Controllers
{

    [AllowAnonymous]
    [Route("email")]
    public class EmailController : Controller
    {
        public INewsLetterService _newsLetterManager;

        public EmailController(INewsLetterService newsLetterManager)
        {
            _newsLetterManager = newsLetterManager;
        }

        [HttpGet]
        [Route("confirm/{email}", Name = "confirm-email-on-subscribe")]
        public async Task<IActionResult> ConfirmEmail(string email)
        {

            if (await _newsLetterManager
                .GetWhere(x => x.MailAddress == email && x.EmailConfirmed == false)
                .FirstOrDefaultAsync() is null)
                return NotFound();

            ViewData["Email"] = email;

            return View();

        }

        [Route("confirm")]
        [HttpPost]
        public async Task<IActionResult> Confirm(NewsletterConfirmSubscriptionViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            NewsLetter? newsLetterSubscriber = await _newsLetterManager
                  .GetWhere(x => x.MailAddress == model.Email)
                  .FirstOrDefaultAsync();

            if (newsLetterSubscriber is null)
                return NotFound();

            newsLetterSubscriber.Name = model.Name;
            newsLetterSubscriber.Surname = model.Surname;
            newsLetterSubscriber.EmailConfirmed = true;
            newsLetterSubscriber.SubscriptionStartDate = DateTime.Now;
            newsLetterSubscriber.SubscriberStatus = SubscriberStatus.Active;

            _newsLetterManager.Update(newsLetterSubscriber);

            return View(nameof(EmailConfirmed));



        }

        [Route("EmailConfirmed")]
        public IActionResult EmailConfirmed()
        {
            return View();
        }
    }
}

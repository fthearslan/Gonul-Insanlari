using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ViewModelLayer.ViewModels.Email;
using ViewModelLayer.ViewModels.Newsletter;

namespace GonulInsanlari.Controllers
{

    [AllowAnonymous]
    [Route("newsletter")]
    public class NewsletterController : Controller
    {
        private readonly INewsLetterService _newsLetterManager;
        private readonly IEmailService _emailManager;
        private readonly IMapper _mapper;
        public NewsletterController(IEmailService emailService, IMapper mapper, INewsLetterService newsLetterManager)
        {
            _emailManager = emailService;
            _mapper = mapper;
            _newsLetterManager = newsLetterManager;
        }

        [Route("subscribe")]
        public async Task<IActionResult> Subscribe(NewsletterSubscribeUIViewModel model)
        {

            if (!ModelState.IsValid)
            {
                List<string>? errors = ModelState.GetModelErrors();

                return BadRequest(errors);

            }

            NewsLetter newsletterSubscriber = _mapper.Map<NewsLetter>(model);

            await _newsLetterManager.AddAsync(newsletterSubscriber);

            if (await _emailManager.SendSubscriptionConfirmationAsync(new SendConfirmEmailViewModel(model.MailAddress, "confirm-email-on-subscribe", HttpContext)
            { Subject = "Confirm Your Subscription" }))
                return StatusCode(200);

            return NotFound();


        }
    }
}

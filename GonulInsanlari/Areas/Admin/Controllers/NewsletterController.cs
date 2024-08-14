using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using ViewModelLayer.ViewModels.Newsletter;
using GonulInsanlari.Enums;
using GonulInsanlari.Extensions;
using ViewModelLayer.Models.Newsletter;
using ViewModelLayer.ViewModels.Email;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Authorize]
    [Area(nameof(Admin))]
    [Route("subscriptions")]
    public class NewsletterController : Controller
    {
        private readonly INewsLetterService _manager;
        private readonly IMapper _mapper;
        public NewsletterController(INewsLetterService manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }


        #region READ

        [Route("list")]
        [HasPermission(PermissionType.Subscription, Permission.Read)]
        public IActionResult Index()
        {
            List<NewsLetter> list = _manager.GetWhere(x => x.Status == true)
                .OrderByDescending(x => x.Created)
                .ToList();

            List<NewsletterListSubscribersViewModel> model = _mapper.Map<List<NewsletterListSubscribersViewModel>>(list);

            return View(model);

        }

        #endregion


        #region CREATE

        [HasPermission(PermissionType.Subscription, Permission.Create)]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateSubscriber([FromServices] IEmailService _emailManager, NewsletterSubscriberCreateViewModel model)
        {

            if (!ModelState.IsValid)
            {
                List<string>? errors = ModelState.GetModelErrors();

                return BadRequest(errors);
            }

            NewsLetter newsLetterSubscriber = _mapper.Map<NewsLetter>(model);

            await _manager.AddAsync(newsLetterSubscriber);

            if (await _emailManager.SendSubscriptionConfirmationAsync(new SendConfirmEmailViewModel(model.MailAddress, "ConfirmEmailOnSubscription", "Email", HttpContext)
            { Subject = "Confirm Your Subscription" }))
                return StatusCode(200);

            return BadRequest(new List<string>() { "Something went wrong while sending confirmation email to the subscriber, please try again." });

        }



        #endregion




        #region UPDATE

        [HttpPost]
        [Route("pendOrDelete/{id}")]
        public async Task<IActionResult> PendOrDelete(int id, string action)
        {

            NewsLetter newsLetterSubscriber = await _manager.GetByIdAsync(id);

            if (newsLetterSubscriber is null)
                return NotFound();

            switch (action)
            {
                case "activated":

                    if (!newsLetterSubscriber.EmailConfirmed)
                        return BadRequest();

                    newsLetterSubscriber.SubscriberStatus = SubscriberStatus.Active;
                    newsLetterSubscriber.SubscriptionEndDate = null;
                    _manager.Update(newsLetterSubscriber);

                    break;

                case "pended":

                    newsLetterSubscriber.SubscriberStatus = SubscriberStatus.Pending;
                    newsLetterSubscriber.SubscriptionEndDate = DateTime.Now;
                    _manager.Update(newsLetterSubscriber);

                    break;

                case "deleted":
                    _manager.Delete(newsLetterSubscriber);

                    break;
            }

            return StatusCode(200, action);

        }

        #endregion



    }
}

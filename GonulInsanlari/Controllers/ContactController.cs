using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModelLayer.ViewModels.Contact;

namespace GonulInsanlari.Controllers
{

    [AllowAnonymous]
    [Route("contact")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactManager;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactManager, IMapper mapper)
        {
            _contactManager = contactManager;
            _mapper = mapper;
        }

        [Route("contact-us")]
        public IActionResult ContactUs()
        {

            return View();
        }

        [HttpPost]
        [Route("submit")]
        public async Task<IActionResult> SubmitContact(SubmitContactViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetModelErrors());

            Contact contact = _mapper.Map<Contact>(model);

            await _contactManager.AddAsync(contact);

            return Ok();

        }


    }
}

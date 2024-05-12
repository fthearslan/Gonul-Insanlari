using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.Tools;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Contact;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using X.PagedList;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ContactController : Controller
    {
        private readonly IContactService _manager;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactController> _logger;
        ResponseModel _response;

        public ContactController(IContactService manager, IMapper mapper, ILogger<ContactController> logger, ResponseModel response)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
            _response = response;
        }

        public async Task<IActionResult> Inbox(int pageNumber = 1)
        {
            List<Contact> contacts = await _manager.GetInbox();
            List<ContactInboxViewModel> model = new();
            try
            {
                model = _mapper.Map<List<ContactInboxViewModel>>(contacts);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

            ViewData["Count"] = model.Count;

            return View(await model.ToPagedListAsync(pageNumber, 15));
        }

        public PartialViewResult Categories()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Refresh()
        {
            List<Contact> contacts = await _manager.GetInbox();
            List<ContactInboxViewModel> model = new();
            try
            {
                model = _mapper.Map<List<ContactInboxViewModel>>(contacts);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

            model.ForEach(x => x.CreatedDate = GetDate.GetCreateDate(x.Created));

            return Json(model.ToPagedList());
        }

        public IActionResult GetDetails()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(List<int>? ids)
        {
            if (ids is not null && ids.Count > 0)
            {
                foreach (var id in ids)
                {
                    var contact = await _manager.GetByIdAsync(id);

                    if (contact is not null)
                    {
                        if (!contact.IsSeen)
                        {
                            contact.IsSeen = true;

                            _manager.Update(contact);

                            _response.responseMessage = "Mails have been successfully marked as read.";
                            _response.success = true;

                        }


                    }
                    else
                    {
                        _response.responseMessage = "Something went wrong.";
                        _response.success = false;

                    }


                }
            }
            else
            {
                _response.responseMessage = "Please, select at least one mail.";
                _response.success = false;

            }

            if (_response.responseMessage is null)
                _response.responseMessage = "Please, select unread mails.";

            return Json(new
            {
                success = _response.success,
                responseMessage = _response.responseMessage
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(List<int>? ids)
        {
            if (ids is not null && ids.Count > 0)
            {
                foreach (var id in ids)
                {
                    var contact = await _manager.GetByIdAsync(id);

                    if (contact is not null)
                    {
                        if (contact.Status == false)
                        {
                            _manager.Delete(contact);

                        }
                        else
                        {
                            contact.Status = false;
                            _manager.Update(contact);
                        }

                        _response.responseMessage = "Mails have been successfully deleted.";
                        _response.success = true;

                    }
                    else
                    {
                        _response.responseMessage = "Something went wrong.";
                        _response.success = false;

                    }


                }
            }
            else
            {
                _response.responseMessage = "Please, select at least one mail.";
                _response.success = false;

            }


            return Json(new
            {
                success = _response.success,
                responseMessage = _response.responseMessage
            });


        }

    }
}

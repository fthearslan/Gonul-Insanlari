using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.Tools;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Contact;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using X.PagedList;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route("mail")]
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

        #region Create 


        #endregion

        #region Read

        [Route("inbox")]
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

            ViewData["Count"] = contacts.Count;


            return View(await model.ToPagedListAsync(pageNumber, 20));
        }

        [Route("detail/{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            Contact contact = await _manager.GetByIdAsync(id);

            if (contact is not null)
                try
                {
                    var model = _mapper.Map<ContactDetailsViewModel>(contact);
                    return View(model);


                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }


            return NotFound();

        }

        [Route("refresh")]
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

            return Json(model.Take(20));
        }

        [Route("search")]
        [HttpPost]
        public async Task<IActionResult> Search(string search)
        {

            List<Contact> result = await _manager.SearchByAsync(search);

            if (result is not null)
            {
                List<ContactInboxViewModel> model = new();

                try
                {
                    model = _mapper.Map<List<ContactInboxViewModel>>(result);
                }
                catch (AutoMapperMappingException ex)
                {
                    _logger.LogError(ex.Message);

                    return BadRequest();
                }

                model.ForEach(x => x.CreatedDate = GetDate.GetCreateDate(x.Created));

                return Json(model);


            }

            return Json(null);

        }



        #endregion

        #region Update

        [Route("markasread")]
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


        #endregion

        #region Delete

        [Route("delete")]
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

        [Route("delete/{id}")]
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var contacttoDelete = await _manager.GetByIdAsync(id);

            if (contacttoDelete is not null)
            {
                if (contacttoDelete.Status == true)
                {
                    contacttoDelete.Status = false;

                    _manager.Update(contacttoDelete);

                }
                else
                {
                    _manager.Delete(contacttoDelete);

                }

            }
            return RedirectToAction(nameof(Inbox));


        }
        #endregion

        #region Partial
        public PartialViewResult Categories()
        {

            return PartialView();
        }

        #endregion


    }
}

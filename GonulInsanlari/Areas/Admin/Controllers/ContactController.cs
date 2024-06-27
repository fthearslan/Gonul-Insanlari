using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Authorization;
using GonulInsanlari.Enums;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Runtime.CompilerServices;
using ViewModelLayer.Models.Tools;
using ViewModelLayer.ViewModels.Contact;
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
        private readonly UserManager<AppUser> _userManager;

        ResponseModel _response;

        public ContactController(IContactService manager, IMapper mapper, ILogger<ContactController> logger, ResponseModel response, UserManager<AppUser> userManager)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
            _response = response;
            _userManager = userManager;
        }

        #region Create 

        [Route("reply/{contactId}")]
        [HttpGet]

        public async Task<IActionResult> Reply(int contactId)
        {

            Contact contact = await _manager.GetByIdAsync(contactId);

            return View(contact);
        }


        [HttpPost]

        public async Task<IActionResult> Reply(Contact model)
        {

            return View();
        }



        #endregion

        #region Read

        [Route("inbox")]
        [HasPermission(PermissionType.Contact, Permission.Read)]
        public async Task<IActionResult> Inbox(int pageNumber = 1)
        {
            List<Contact> contacts = await _manager.GetInboxAsync();
            List<ContactListViewModel> model = new();
            try
            {
                model = _mapper.Map<List<ContactListViewModel>>(contacts);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

            ViewData["Count"] = contacts.Count;


            return View(await model.ToPagedListAsync(pageNumber, 20));
        }


        [Route("sentbox")]
        [HasPermission(PermissionType.Contact, Permission.Read)]
        public async Task<IActionResult> SentBox(int pageNumber = 1)
        {
            string _userId = _userManager.GetUserId(HttpContext.User);

            List<Contact> contacts = await _manager.GetSentboxAsync(_userId);
            List<ContactListViewModel> model = new();

            if (contacts is not null)
                try
                {
                    model = _mapper.Map<List<ContactListViewModel>>(contacts);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();


                }

            model.ForEach(x => x.CreatedDate = GetDate.GetCreateDate(x.Created));

            ViewData["Count"] = model.Count;

            return View(await model.ToPagedListAsync(pageNumber, 20));

        }

        [Route("drafts")]
        [HasPermission(PermissionType.Contact, Permission.Read)]
        public async Task<IActionResult> Drafts(int pageNumber = 1)
        {

            string _userId = _userManager.GetUserId(HttpContext.User);

            List<Contact> drafts = await _manager.GetDraftsAsync(_userId);

            List<ContactListViewModel> model = new();
            try
            {
                model = _mapper.Map<List<ContactListViewModel>>(drafts);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return BadRequest();
            }

            ViewData["Count"] = model.Count;

            model.ForEach(x => x.CreatedDate = GetDate.GetCreateDate(x.Created));

            return View(await model.ToPagedListAsync(pageNumber, 20));

        }



        [Route("detail/{id}")]
        [HasPermission(PermissionType.Contact, Permission.Read)]
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
            List<Contact> contacts = await _manager.GetInboxAsync();

            List<ContactListViewModel> model = new();
            try
            {
                model = _mapper.Map<List<ContactListViewModel>>(contacts);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

            model.ForEach(x => x.CreatedDate = GetDate.GetCreateDate(x.Created));

            return Json(model.Take(20));
        }


        [Route("refreshsentbox")]
        [HttpPost]
        public async Task<IActionResult> RefreshSent(string status)
        {


            string userId = _userManager.GetUserId(HttpContext.User);
            List<Contact> contacts = new();

            switch (status)
            {
                case "sent":
                    contacts = await _manager.GetSentboxAsync(userId);
                    break;
                case "trash":
                    contacts = await _manager.GetTrashAsync();
                    break;
                case "draft":
                    contacts = await _manager.GetDraftsAsync(userId);
                    break;
            }






            List<ContactListViewModel> model = new();

            try
            {
                model = _mapper.Map<List<ContactListViewModel>>(contacts);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

            model.ForEach(x => x.CreatedDate = GetDate.GetCreateDate(x.Created));

            return Json(model.Take(20));
        }


        [Route("trash")]
        [HasPermission(PermissionType.Contact, Permission.Read)]
        public async Task<IActionResult> GetTrash(int pageNumber = 1)
        {

            List<Contact> contactsToDelete = await _manager.GetTrashAsync();

            List<ContactListViewModel> model = new();

            try
            {
                model = _mapper.Map<List<ContactListViewModel>>(contactsToDelete);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return BadRequest();
            }

            model.ForEach(x => x.CreatedDate = GetDate.GetCreateDate(x.Created));

            ViewData["Count"] = model.Count;

            return View(await model.ToPagedListAsync(pageNumber, 20));

        }




        [Route("search")]
        [HttpPost]
        public async Task<IActionResult> Search(string search, bool isSent, bool isdraft, bool isTodelete)
        {
            List<Contact> result = new();


            string _senderId = _userManager.GetUserId(HttpContext.User);

            result = await _manager.SearchByAsync(search, _senderId, isdraft, isTodelete, isSent);


            if (result is not null)
            {
                List<ContactListViewModel> model = new();

                try
                {
                    model = _mapper.Map<List<ContactListViewModel>>(result);
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
        [HasPermission(PermissionType.Contact, Permission.Update)]
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
        [HasPermission(PermissionType.Contact, Permission.Update | Permission.Delete)]

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
        [HasPermission(PermissionType.Contact, Permission.Update |Permission.Delete)]

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

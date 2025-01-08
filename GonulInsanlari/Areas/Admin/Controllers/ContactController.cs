using AutoMapper;
using BussinessLayer.Abstract.Services;
using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Authorization;
using GonulInsanlari.Enums;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Caching.Memory;
using RabbitMQ.Client;
using System.Runtime.CompilerServices;
using ViewModelLayer.Models.Newsletter;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailManager;
        private readonly IMemoryCache _cache;

        ResponseModel _response;

        public ContactController(IContactService manager, IMapper mapper, ResponseModel response, UserManager<AppUser> userManager, IEmailService emailManager, IMemoryCache cache)
        {
            _manager = manager;
            _mapper = mapper;
            _response = response;
            _userManager = userManager;
            _emailManager = emailManager;
            _cache = cache;
        }

        #region Create 





        [HttpGet]
        [Route("compose")]
        public IActionResult Compose()
        {

            return View();

        }








        [HttpPost]
        [Route("compose")]
        [HasPermission(PermissionType.Contact, Permission.Create)]
        public async Task<IActionResult> Compose(SendMailModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> paths = await model.GetAttachmentPathsAsync();

                Contact contact = new(model.Status)
                {
                    From = "Administration",

                    Tos = model.GetContactTos(),
                    Status = true,
                    Subject = model.Subject,
                    SenderId = _userManager.GetUserId(User),
                    Content = model.Content,

                };

                paths?.ForEach((path) =>
                {
                    contact.Attachments
                    .Add(new(path));
                });

                await _manager.AddAsync(contact);

                if (model.Status == ContactStatus.Sent)
                    await _emailManager.SendEmailAsync(model);


                return RedirectToAction(nameof(GetDetails), new { contact.Id });

            }

            return View(model);

        }






        [Route("reply/{contactId}")]
        [HttpGet]
        public async Task<IActionResult> Reply(int contactId)
        {
            Contact contactToReply = await _manager.GetByIdAsync(contactId);

            if (contactToReply is null)
                return NotFound();

            if (contactToReply.ContactStatus == ContactStatus.Drafted | contactToReply.ContactStatus == ContactStatus.Sent)
                return NotFound();

            ContactDetailsViewModel model = _mapper.Map<ContactDetailsViewModel>(contactToReply);

            ViewData["Reply"] = model;

            _cache.Set("reply", model);


            return View();

        }






        [HttpPost]
        [Route("reply/{contactId}")]
        [HasPermission(PermissionType.Contact, Permission.Create)]
        public async Task<IActionResult> Reply(SendMailModel model)
        {

            if (model.To.Count > 1)
                ModelState.AddModelError(nameof(model.To), "You can not reply more than one mail.");

            if (ModelState.IsValid)
                return await Compose(model);

            ViewData["Reply"] = _cache.Get("reply");

            return View(model);


        }



        #endregion

        #region Read



        [Route("inbox")]
        [HasPermission(PermissionType.Contact, Permission.Read)]
        public async Task<IActionResult> Inbox(int pageNumber = 1)
        {


            List<Contact> contacts = await _manager
                .GetWhere(x => x.ContactStatus == ContactStatus.Received && x.Status == true)
                .OrderByDescending(x => x.Created)
                .ToListAsync();


            List<ContactListViewModel> model = new();

            model = _mapper.Map<List<ContactListViewModel>>(contacts);


            ViewData["Count"] = contacts.Count;


            return View(await model.ToPagedListAsync(pageNumber, 20));
        }



        [Route("sentbox")]
        [HasPermission(PermissionType.Contact, Permission.Read)]
        public async Task<IActionResult> SentBox(int pageNumber = 1)
        {
            List<Contact> contacts = await _manager
             .GetWhere(x => x.ContactStatus == ContactStatus.Sent && x.SenderId == _userManager.GetUserId(HttpContext.User))
                .OrderByDescending(x => x.Created)
                .ToListAsync();


            List<ContactListViewModel> model = new();

            if (contacts is null)
                return NotFound();

            model = _mapper.Map<List<ContactListViewModel>>(contacts);

            model.ForEach(x => x.CreatedDate = GetDate.GetCreateDate(x.Created));

            ViewData["Count"] = model.Count;

            return View(await model.ToPagedListAsync(pageNumber, 20));

        }


        [Route("drafts")]
        [HasPermission(PermissionType.Contact, Permission.Read)]
        public async Task<IActionResult> Drafts(int pageNumber = 1)
        {

            List<Contact> drafts = await _manager
                .GetWhere(x => x.ContactStatus == ContactStatus.Drafted && x.SenderId == _userManager.GetUserId(HttpContext.User))
                .OrderByDescending(x => x.Created)
                .ToListAsync();


            List<ContactListViewModel> model = new();

            model = _mapper.Map<List<ContactListViewModel>>(drafts);

            ViewData["Count"] = model.Count;

            model.ForEach(x => x.CreatedDate = GetDate.GetCreateDate(x.Created));

            return View(await model.ToPagedListAsync(pageNumber, 20));

        }


        [Route("trash")]
        [HasPermission(PermissionType.Contact, Permission.Read)]
        public async Task<IActionResult> GetTrash(int pageNumber = 1)
        {

            List<Contact> contactsToDelete = await _manager.GetWhere(x => x.Status == false)
                .OrderByDescending(x => x.Created)
                .ToListAsync();

            List<ContactListViewModel> model = _mapper.Map<List<ContactListViewModel>>(contactsToDelete);

            model.ForEach(x => x.CreatedDate = GetDate.GetCreateDate(x.Created));

            ViewData["Count"] = model.Count;

            return View(await model.ToPagedListAsync(pageNumber, 20));

        }

        [Route("newsletters")]
        [HasPermission(PermissionType.Contact, Permission.Read)]
        public async Task<IActionResult> Newsletters(int pageNumber = 1)
        {

            List<Contact> contacts = await _manager
                .GetWhere(x => x.ContactStatus == ContactStatus.Newsletter && x.Status == true)
                .ToListAsync();

            List<ContactListViewModel> model = _mapper.Map<List<ContactListViewModel>>(contacts);

            ViewData["Count"] = model.Count;

            return View(await model.ToPagedListAsync(pageNumber, 20));


        }


        [Route("all")]
        [HasPermission(PermissionType.Contact, Permission.Read)]
        public IActionResult GetAll()
        {

            List<ContactListViewModel> model = _mapper.Map<List<ContactListViewModel>>(_manager.List());

            return View(model);


        }




        [Route("detail/{id}")]
        [HasPermission(PermissionType.Contact, Permission.Read)]
        public async Task<IActionResult> GetDetails(int id)
        {

            Contact contact = await _manager.GetWithReply(id);

            if (contact is null)
                return NotFound();

            contact.IsSeen = true;

            _manager.Update(contact);

            var model = _mapper.Map<ContactDetailsViewModel>(contact);

            return View(model);


        }





        [Route("refresh")]
        [HttpPost]
        public async Task<IActionResult> Refresh(ContactStatus status)
        {

            List<Contact> contacts = status switch
            {
                ContactStatus.Received or ContactStatus.Newsletter => await _manager.GetWhere(x => x.ContactStatus == status && x.Status == true).ToListAsync(),

                ContactStatus.Trash => await _manager.GetWhere(x => x.ContactStatus == status && x.Status == false).ToListAsync(),

                _ => await _manager.GetWhere(x => x.ContactStatus == status && x.SenderId == _userManager.GetUserId(HttpContext.User) && x.Status == true).ToListAsync()
            };


            List<ContactListViewModel> model = new();

            model = _mapper.Map<List<ContactListViewModel>>(contacts);


            model.ForEach(x => x.CreatedDate = GetDate.GetCreateDate(x.Created));

            return Json(model.Take(20));
        }



        [Route("search")]
        [HttpPost]
        public async Task<IActionResult> Search(ContactSearchViewModel model)
        {

            string _senderId = _userManager.GetUserId(HttpContext.User);

            List<Contact> contacts = model.GetAll switch
            {
                true => await _manager.SearchByAsync(model.Search),
                false => await _manager.SearchByAsync(model.Search, model.ContactStatus, _senderId)
            };

            if (contacts is not null)
            {

                List<ContactListViewModel> viewModel = _mapper.Map<List<ContactListViewModel>>(contacts);

                viewModel.ForEach(x => x.CreatedDate = GetDate.GetCreateDate(x.Created));

                return Json(viewModel);

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


        [Route("recover/{id}")]
        [HttpGet]
        [HasPermission(PermissionType.Contact, Permission.Update)]
        public async Task<IActionResult> Recover(int id)
        {

            Contact trash = await _manager.GetByIdAsync(id);


            if (trash is null || trash.Status is not null && (bool)trash.Status)
                return NotFound();

            trash.Status = true;

            _manager.Update(trash);

            return RedirectToAction(nameof(GetDetails), new { id });


        }

        [Route("edit/{id}")]
        [HttpGet]
        [HasPermission(PermissionType.Contact, Permission.Update)]
        public async Task<IActionResult> EditDraft(int id)
        {
            Contact draft = await _manager.GetByIdAsync(id);

            if (draft is null)
                return NotFound();

            SendMailModel model = new()
            {
                Id = id,
                Content = draft.Content,
                ReplyTo = draft.RepliedTo?.Id,
                Subject = draft.Subject,
                Status = draft.ContactStatus,
                AttachmentExists = draft.Attachments?.Select(x => x.Path).ToList(),
                To = draft.Tos
                .Select(x => x.EmailAddress)
                .ToList(),
            };

            return View(model);

        }

        [Route("edit/{id}")]
        [HttpPost]
        [HasPermission(PermissionType.Contact, Permission.Update)]
        public async Task<IActionResult> EditDraft(SendMailModel model)
        {
            Contact? contact = await _manager.GetByIdAsync(model.Id);

            if (ModelState.IsValid)
            {
                List<string> paths = await model.GetAttachmentPathsAsync();



                if (contact is null)
                    return NotFound();

                contact.From = "Administration";
                contact.Subject = model.Subject;
                contact.SenderId = _userManager.GetUserId(User);
                contact.Content = model.Content;
                contact.ContactStatus = model.Status;

                if (contact.ContactStatus == ContactStatus.Sent)
                    contact.Created = DateTime.Now;

                paths?.ForEach((path) =>
                {
                    contact.Attachments
                    .Add(new(path));
                });

                List<string> mailsExists = contact.Tos.
                    Select(x => x.EmailAddress).
                    ToList();

                model.To.ForEach((adr) =>
                {

                    if (!mailsExists.Contains(adr))
                        contact.Tos.Add(new(adr) { Contact = contact });

                });

                contact.Tos.RemoveAll(x => !model.To.Contains(x.EmailAddress));

                _manager.Update(contact);

                if (contact.ContactStatus == ContactStatus.Sent)
                {
                    await _emailManager.SendEmailAsync(model);

                    return RedirectToAction(nameof(SentBox));

                }

                return RedirectToAction(nameof(GetDetails), new { contact.Id });

            }


            List<string> emails = new List<string>();

            contact.Tos?.ForEach((x) =>
            {
                emails.Add(x.EmailAddress);
            });

            model.To = emails;

            return View(model);

        }


        #endregion

        #region Delete

        [Route("delete")]
        [HttpPost]
        [HasPermission(PermissionType.Contact, Permission.Delete)]
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
        [HasPermission(PermissionType.Contact, Permission.Delete)]
        public async Task<IActionResult> Delete(int id)
        {

            await Delete(new List<int>() { id });

            return RedirectToAction(nameof(GetTrash));


        }

        [Route("removeFile")]
        [HttpPost]
        [HasPermission(PermissionType.Contact, Permission.Delete)]
        public async Task<IActionResult> RemoveFile(string fileName, int Id)
        {

            Contact contact = await _manager.GetByIdAsync(Id);

            if (contact is null)
                return NotFound();

            ContactAttachment? attachment = contact.
                 Attachments?.
                 Find(x => x.Path == fileName);

            if (attachment is null)
                return NotFound();

            contact.
                Attachments?.Remove(attachment);

            if (_manager.GetWhere(x => x.Attachments != null && x.Attachments.Any(a => a.Path.Contains(fileName))).ToList().Count > 0)
            {

                _manager.Update(contact);
                return Json(true);

            }
            else
            {
                return Json(ImageUpload.DeleteFile(fileName));

            }

        }

        #endregion

        #region Methods 

        [Route("downloadFile/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {

            var memory = new MemoryStream();

            string fileNameTrimmed = fileName.Replace(" ", "");

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/", fileNameTrimmed);

            if (!System.IO.File.Exists(path))
                return NotFound();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);

            }

            memory.Position = 0;
            string ext = Path.GetExtension(path).ToLowerInvariant();

            return File(memory, ImageUpload.GetMimeTypes()[ext], Path.GetFileName(path));

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



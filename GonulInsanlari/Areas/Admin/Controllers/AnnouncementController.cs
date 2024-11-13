using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using BussinessLayer.Concrete.Validations;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
using FluentValidation;
using GonulInsanlari.Areas.Admin.Authorization;
using GonulInsanlari.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using ViewModelLayer.ViewModels.Announcement;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route("announcements")]
    public class AnnouncementController : Controller
    {

        #region DI Services

        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAnnouncementService _manager;

        public AnnouncementController(IMapper mapper, UserManager<AppUser> UserManager, IMemoryCache cache, IAnnouncementService manager)
        {
            _mapper = mapper;
            _userManager = UserManager;
            _memoryCache = cache;
            _manager = manager;
        }

        #endregion

        #region CREATE

        [Route("add")]
        [HttpGet]
        public IActionResult AddAnnouncement()
        {
            return View();
        }

        [Route("add")]
        [HttpPost]
        [HasPermission(PermissionType.Announcement, Permission.Create)]
        public async Task<IActionResult> AddAnnouncement(AnnouncementCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                Announcement entity = _mapper.Map<Announcement>(model);

                entity.User = user;

                _manager.InsertWithRelated(entity);

                return RedirectToAction(nameof(GetDetails), new { id = entity.Id });
            }

            return View(model);

        }


        #endregion

        #region READ


        [Route("list")]
        [HasPermission(PermissionType.Announcement, Permission.Read)]
        public IActionResult List()
        {
            var list = _manager.List();

            List<AnnouncementListViewModel> model = _mapper.Map<List<AnnouncementListViewModel>>(list);


            return View(model);



        }

        [Route("details/{id}")]
        [HasPermission(PermissionType.Announcement, Permission.Read)]

        public async Task<IActionResult> GetDetails(int id)
        {

            var announcement = await _manager.GetByIdAsync(id);

            if (announcement is null)
                return NotFound();

            var model = _mapper.Map<AnnouncementDetailsViewModel>(announcement);


            return View(model);




        }

        #endregion

        #region UPDATE

        [Route("edit/{id}")]
        [HttpGet]

        public async Task<IActionResult> EditAnnouncement(int id)
        {
            var announcement = await _manager.GetWithUserAsync(id);

            if (announcement != null)
            {
                var model = _mapper.Map<AnnouncementEditViewModel>(announcement);
                return View(model);

            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        [HasPermission(PermissionType.Announcement, Permission.Update)]

        public async Task<IActionResult> EditAnnouncement(AnnouncementEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                Announcement entity = _mapper.Map<Announcement>(model);
                entity.EditedBy = user.UserName;

                _manager.Update(entity);

                return RedirectToAction(nameof(GetDetails), entity.Id);

            }

            return View(model);
        }


        [Route("attach/{id}")]
        [HttpPost]
        public async Task<IActionResult> Attach(int id)
        {
           
            Announcement announcementToAttach = await _manager.GetByIdAsync(id);

            string action = "";

            switch (announcementToAttach.IsAttached)
            {
                case true:
                    announcementToAttach.IsAttached = false;
                    action = "Dettached";
                    break;
                case false:
                    announcementToAttach.IsAttached = true;
                    action = "Attached";
                    break;

            }

            var announcement = await _manager.GetWhere(x => x.IsAttached == true)
               .SingleOrDefaultAsync();

            if (announcement is not null)
            {
                announcement.IsAttached = false;

                _manager.Update(announcement);

            }

            _manager.Update(announcementToAttach);

            return StatusCode(200,action);


        }


        #endregion

        #region DELETE

        [HttpPost]
        [Route("delete/{id}")]
        [HasPermission(PermissionType.Announcement, Permission.Delete)]
        public async Task<IActionResult> Delete(int id)
        {

            Announcement announcementTodelte = await _manager.GetByIdAsync(id);

            if (announcementTodelte is not null)
                _manager.Delete(announcementTodelte);

            return RedirectToAction(nameof(List));

        }


        #endregion


    }

}

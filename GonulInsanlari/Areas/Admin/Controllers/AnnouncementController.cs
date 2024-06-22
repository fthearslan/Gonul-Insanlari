﻿using AutoMapper;
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
using Microsoft.Build.Framework;
using Microsoft.Extensions.Caching.Memory;
using Org.BouncyCastle.Crypto;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using ViewModelLayer.ViewModels.Announcement;
using static iTextSharp.text.pdf.events.IndexEvents;

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
        public async Task<IActionResult> AddAnnouncement(AnnouncementCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                Announcement entity = _mapper.Map<Announcement>(model);
                entity.User = user;

                _manager.InsertWithRelated(entity);

                return RedirectToAction("GetDetails", entity.Id);
            }

            return View(model);

        }


        #endregion

        #region READ

        
        [Route("list")]
        public async Task<IActionResult> List()
        {
            var list = await _manager.GetForAdminAsync();
            if (list.Count > 0)
            {
                List<AnnouncementListViewModel> model = _mapper.Map<List<AnnouncementListViewModel>>(list);
                return View(model);

            }

            return NotFound();

        }

        [Route("details/{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {

            var announcement = await _manager.GetWithUserAsync(id);

            if (announcement != null)
            {
                var model = _mapper.Map<AnnouncementDetailsViewModel>(announcement);

                ViewBag.IsUser = (announcement.User == await _userManager.GetUserAsync(HttpContext.User)) ? true : false;

                return View(model);

            }

            return NotFound();


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


        #endregion

        #region DELETE

        [HttpPost]
        [Route("delete/{id}")]
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

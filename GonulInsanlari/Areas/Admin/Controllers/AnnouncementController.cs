using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using BussinessLayer.Concrete.Validations;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
using FluentValidation;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Announcement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class AnnouncementController : Controller
    {

        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        private readonly ILogger<AnnouncementController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAnnouncementService _manager;
        private readonly AbstractValidator<Announcement> _validator;

        public AnnouncementController(IMapper mapper, ILogger<AnnouncementController> logger, UserManager<AppUser> UserManager, IMemoryCache cache, IAnnouncementService manager, AbstractValidator<Announcement> validator)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = UserManager;
            _memoryCache = cache;
            _manager = manager;
            _validator = validator;
        }

        public async Task<IActionResult> List()
        {
            var list = await _manager.GetForAdminAsync();
            if (list.Count > 0)
            {


                try
                {
                    List<AnnouncementListViewModel> model = _mapper.Map<List<AnnouncementListViewModel>>(list);
                    return View(model);

                }
                catch (AutoMapperMappingException)
                {
                    _logger.LogError("AutoMapper exception has been thrown on List ActionMethod of AnnouncementController");
                    return View(); // notFound page...
                }
            }
            return View();
        }


        public async Task<IActionResult> GetDetails(int id)
        {
            var announcement = await _manager.GetWithUserAsync(id);
            if (announcement != null)
                try
                {
                    var model = _mapper.Map<AnnouncementDetailsViewModel>(announcement);
                    return View(model);
                }
                catch (AutoMapperMappingException)
                {
                    _logger.LogError("Mappign exception has been thrown at Admin/Announcement/GetDetails");
                }
            
                return RedirectToAction(nameof(List));


        }

        [HttpGet]
        public IActionResult AddAnnouncement()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAnnouncement(AnnouncementCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    Announcement entity = _mapper.Map<Announcement>(model);

                    var result = await _validator.ValidateAsync(entity);
                    if (result.IsValid)
                    {
                        var user = await _userManager.GetUserAsync(HttpContext.User);
                        if (user is not null)
                            entity.User = user;
                        _manager.InsertWithRelated(entity);
                        return RedirectToAction(nameof(List));
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
                catch (AutoMapperMappingException)
                {
                    _logger.LogError("AutoMapper exception has been thrown on AddAnnouncement ActionMethod of AnnouncementController");
                    return RedirectToAction(nameof(List)); // ErrorPage...
                }

            }

            return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> EditAnnouncement(int id)
        {
            var announcement = await _manager.GetWithUserAsync(id);
            if (announcement != null)
            {

                try
                {
                    var model = _mapper.Map<AnnouncementEditViewModel>(announcement);
                    return View(model);
                }
                catch (AutoMapperMappingException)
                {
                    _logger.LogError("AutoMapper exception has been thrown on EditAnnouncement ActionMethod of AnnouncementController");

                    return RedirectToAction(nameof(List));
                }

            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAnnouncement(AnnouncementEditViewModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    Announcement entity = _mapper.Map<Announcement>(model);

                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    entity.EditedBy = user.UserName;

                    _manager.Update(entity);
                    return RedirectToAction(nameof(List));
                }
                catch (AutoMapperMappingException)
                {

                    _logger.LogError("AutoMapper exception has been thrown on EditAnnouncement ActionMethod of AnnouncementController");
                    return View(model);
                }


            }

            return View(model);
        }


    }

}

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
    [Route("announcements")]
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

        [Route("list")]

        public async Task<IActionResult> List()
        {
            var list = await _manager.GetForAdminAsync();
            if (list.Count > 0)
                try
                {
                    List<AnnouncementListViewModel> model = _mapper.Map<List<AnnouncementListViewModel>>(list);
                    return View(model);

                }
                catch (AutoMapperMappingException)
                {
                    _logger.LogError("AutoMapper exception has been thrown on List ActionMethod of AnnouncementController");
                    return BadRequest();
                }

            return NotFound();

        }

        [Route("details/{id}")]

        public async Task<IActionResult> GetDetails(int id)
        {



            var announcement = await _manager.GetWithUserAsync(id);

            if (announcement != null)
                try
                {
                    var model = _mapper.Map<AnnouncementDetailsViewModel>(announcement);

                    ViewBag.IsUser = (announcement.User == await _userManager.GetUserAsync(HttpContext.User)) ? true : false;

                    return View(model);
                }
                catch (AutoMapperMappingException ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }

            return NotFound();


        }
        
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
                        return RedirectToAction("GetDetails",entity.Id);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
                catch (AutoMapperMappingException ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }


            return View(model);

        }

        [Route("edit/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditAnnouncement(int id)
        {
            var announcement = await _manager.GetWithUserAsync(id);
            if (announcement != null)
                try
                {
                    var model = _mapper.Map<AnnouncementEditViewModel>(announcement);
                    return View(model);
                }
                catch (AutoMapperMappingException ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();

                }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditAnnouncement(AnnouncementEditViewModel model)
        {
            if (ModelState.IsValid)
                try
                {
                    Announcement entity = _mapper.Map<Announcement>(model);

                    var user = await _userManager.GetUserAsync(HttpContext.User);

                    entity.EditedBy = user.UserName;

                    _manager.Update(entity);
                    return RedirectToAction(nameof(GetDetails), entity.Id);
                }
                catch (AutoMapperMappingException ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();

                }

            return View(model);
        }


    }

}

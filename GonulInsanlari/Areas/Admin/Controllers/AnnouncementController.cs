using AutoMapper;
using BussinessLayer.Concrete;
using BussinessLayer.Concrete.Validations;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Announcement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class AnnouncementController : Controller
    {
        AnnouncementManager _manager = new AnnouncementManager(new EFAnnouncementDAL());
        AnnouncementValidator _validator = new AnnouncementValidator();

        private readonly IMapper _mapper;
        private readonly ILogger<AnnouncementController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public AnnouncementController(IMapper mapper, ILogger<AnnouncementController> logger, UserManager<AppUser> UserManager)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = UserManager;
        }

        public IActionResult List()
        {
            var list = _manager.GetForAdmins();
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
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user is not null)
                    model.CreatedBy = user;

                try
                {
                    Announcement entity = _mapper.Map<Announcement>(model);
                    var result = await _validator.ValidateAsync(entity);
                    if (result.IsValid)
                    {
                        _manager.Add(entity);
                        return RedirectToAction(nameof(List));
                    }
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName,error.ErrorMessage);
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

    }

}

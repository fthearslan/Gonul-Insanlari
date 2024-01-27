using AutoMapper;
using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Announcement;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class AnnouncementController : Controller
    {
        AnnouncementManager _manager = new AnnouncementManager(new EFAnnouncementDAL());
        IMapper _mapper;
        ILogger<AnnouncementController> _logger;

        public AnnouncementController(IMapper mapper, ILogger<AnnouncementController> logger)
        {
            _mapper = mapper;
            _logger = logger;
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
                    _logger.LogError("AutoMapper exception has been thrown on List Action Method of AnnouncementController");
                    return View(); // notFound page...
                }
            }
            return View();
        }

        public IActionResult AddAnnouncement()
        {
            return View();
        }


    }

}

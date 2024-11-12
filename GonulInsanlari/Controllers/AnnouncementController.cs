using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModelLayer.ViewModels.Announcement;

namespace GonulInsanlari.Controllers
{
    [AllowAnonymous]
    [Route("announcement")]
    public class AnnouncementController : Controller
    {

        private readonly IAnnouncementService _announcementManager;
        private readonly IMapper _mapper;

        public AnnouncementController(IAnnouncementService announcementManager, IMapper mapper)
        {
            _announcementManager = announcementManager;
            _mapper = mapper;
        }

        [Route("{slug}/{id}")]
        public async Task<IActionResult> GetDetails(string slug, int id)
        {
            Announcement announcement = await _announcementManager.GetByIdAsync(id);

            if(announcement is  null)
                return NotFound();

            AnnouncementDetailsViewModel model = _mapper.Map<AnnouncementDetailsViewModel>(announcement);

            return View(model);

        }
    }
}

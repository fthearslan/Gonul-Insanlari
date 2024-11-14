using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModelLayer.ViewModels.About;

namespace GonulInsanlari.Controllers
{




    [AllowAnonymous]
    [Route("about-us")]
    public class AboutController : Controller
    {


        private readonly IAboutService _aboutManager;

        public AboutController(IAboutService aboutManager)
        {
            _aboutManager = aboutManager;
        }

        [Route("")]
        public async Task<IActionResult> AboutUs()
        {

            About? about = await _aboutManager.GetWhere(x => x.Status == true)
                 .FirstOrDefaultAsync();

            if (about is null)
                return NotFound();

            AboutDetailsViewModel model = new(about.Id, about.Title, about.Details);

            return View(model);

        }
    }
}

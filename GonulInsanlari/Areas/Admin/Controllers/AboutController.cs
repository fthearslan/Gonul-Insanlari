using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using ViewModelLayer.ViewModels.About;

namespace GonulInsanlari.Areas.Admin.Controllers
{


    [Area(nameof(Admin))]
    [Route("about")]
    public class AboutController : Controller
    {

        private readonly IAboutService _aboutManager;

        public AboutController(IAboutService aboutManager)
        {
            _aboutManager = aboutManager;
        }

        [Route("display")]
        public async Task<IActionResult> About()
        {

            About? about = await _aboutManager.GetWhere(x => x.Status == true)
              .FirstOrDefaultAsync();

            if (about is null)
                return View();

            AboutDetailsViewModel model = new AboutDetailsViewModel(about.Id, about.Title, about.Details);

            return View(model);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> About(EdtiAboutViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetModelErrors());


            About? about = await _aboutManager.GetWhere(x => x.Status == true)
                 .FirstOrDefaultAsync();

            if (about is null)
            {
                About aboutTobeAdded = new()
                {
                    Title = model.Title,
                    Details = model.Details,
                    Status = true,

                };

                await _aboutManager.AddAsync(aboutTobeAdded);

                return StatusCode(200,"added!");
            }

            about.Title = model.Title;
            about.Details = model.Details;
            about.Created = DateTime.Now;

            _aboutManager.Update(about);

            return StatusCode(200,"changed!");

        }



    }
}

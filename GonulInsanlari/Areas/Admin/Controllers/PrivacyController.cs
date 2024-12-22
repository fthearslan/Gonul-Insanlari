using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Extensions;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;
using ViewModelLayer.ViewModels.Privacy;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route("privacy")]
    public class PrivacyController : Controller
    {


        [Route("display")]

        public async Task<IActionResult> Privacy()
        {

            using Context context = new Context();

            Privacy? privacyText = await context.Privacies.
                    FirstOrDefaultAsync(x => x.Status == true);

            if (privacyText is null)
                return View(null);

            PrivacyViewModel privacyViewModel = new(privacyText.Id, privacyText.Title, privacyText.Content);

            return View(privacyViewModel);

        }


        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Privacy(EditPrivacyViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetModelErrors());

            using Context context = new();

            Privacy? privacy = await context.Privacies.FirstOrDefaultAsync(x => x.Id == model.Id && x.Status == true);

            if (privacy is null)
            {
                Privacy privacyToBeAdded = new Privacy();

                privacyToBeAdded.Title = model.Title;
                privacyToBeAdded.Content = model.Content;
                privacyToBeAdded.Status = true;

                await context.Privacies.AddAsync(privacyToBeAdded);
                await context.SaveChangesAsync();
                return StatusCode(200, "added!");

            }

            privacy.Title = model.Title;
            privacy.Content = model.Content;

            context.Privacies.Update(privacy);

            await context.SaveChangesAsync();

            return StatusCode(200, "changed!");

        }

        [HttpPost]
        [Route("getPrivacy")]

        public async Task<IActionResult> GetPrivacy(int id)
        {

            using Context context = new Context();

            Privacy? privacyText = await context.Privacies.
                    FirstOrDefaultAsync(x => x.Id == id);

            if (privacyText is null)
                return Json(null);

                return Ok(new
                {
                    Id = privacyText.Id,
                    Title = privacyText.Title,
                    Content = privacyText.Content
                });


        }


    }
}

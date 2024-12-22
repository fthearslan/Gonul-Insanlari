using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModelLayer.ViewModels.Privacy;

namespace GonulInsanlari.Controllers
{
    [AllowAnonymous]
    [Route("privacy")]
    public class PrivacyController : Controller
    {


        [Route("")]
        public async Task<IActionResult> Privacy()
        {

            using Context context = new();

            Privacy? privacy = await context.Privacies.FirstOrDefaultAsync(x => x.Status == true);

            if (privacy is null)
                return NotFound();

            PrivacyViewModel model = new(privacy.Id,privacy.Title,privacy.Content);

            model.LastUpdated = (privacy.Modified is null) ? privacy.Created : (DateTime)privacy.Modified;

            return View(model);
        
        
        }
    }
}

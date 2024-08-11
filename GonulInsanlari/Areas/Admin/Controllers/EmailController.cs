using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area(nameof(Admin))]
    [Route("mail")]
    public class EmailController : Controller
    {

        [Route("confirmed")]
        public async Task<IActionResult> EmailConfirmed([FromServices] UserManager<AppUser> userManager,string userId, string token)
        {

            if (userId is null || token is null)
                return RedirectToAction("Login");

            AppUser user =  await userManager.FindByIdAsync(userId);

            if(user is null)
                return RedirectToAction("Login");

            user.EmailConfirmed = true;

           IdentityResult result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                ViewData["Success"] = " Email has been successfully confirmed. You can go back to login page to log in.";
            }
            else
            {
                ViewData["Error"] = "Something went wrong while confirming your email, please try again later.";

            }

            return View();
        }
    
    
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Controllers
{

    [AllowAnonymous]
    [Route("email")]
    public class EmailController : Controller
    {


        [HttpGet]
        [Route("confirm/{email}",Name = "confirm-email-on-subscribe")]
        public IActionResult ConfirmEmail(string email)
        {
            return View();
        }

        [HttpPost]
        [Route("confirm")]
        public IActionResult ConfirmEmail(int e)
        {
            return View(e);
        }


    }
}

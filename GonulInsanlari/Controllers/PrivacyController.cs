using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Controllers
{
    [AllowAnonymous]
    [Route("privacy")]
    public class PrivacyController : Controller
    {

        [Route("")]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}

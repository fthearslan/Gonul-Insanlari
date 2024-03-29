using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ErrorController : Controller
    {
        
        public IActionResult SomethingWentWrong(int code)
        {
            return View();
        }
    }
}

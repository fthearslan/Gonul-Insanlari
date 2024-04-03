using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ErrorController : Controller
    {

        public IActionResult ErrorPage(int code)
        {
            switch (code)
            {
                case 400:
                    return View(nameof(SomethingWentWrong));
                case 404:
                    return View();
                case 500:
                    return View(nameof(SomethingWentWrong));
            }

            return View();
        }

        public IActionResult SomethingWentWrong()
        {
            return View();
        }


    }
}

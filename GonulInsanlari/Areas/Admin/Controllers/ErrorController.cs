using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route("error")]
    public class ErrorController : Controller
    {
        [Route("notFound")]
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
                case 403:
                    return View(nameof(AccessDenied));


            }

            return View();
        }

        [Route("internal")]
        public IActionResult SomethingWentWrong()
        {
            return View();
        }

        [Route("accessDenied")]
        public IActionResult AccessDenied()
        {
            return View();


        }



    }
}

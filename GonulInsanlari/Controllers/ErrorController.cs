using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Controllers
{
    [AllowAnonymous]
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

                    if (User.Identity is not null && User.Identity.IsAuthenticated)
                        return View();

                    return View(nameof(NotFoundClient));


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

        [Route("not-found")]
        public IActionResult NotFoundClient()
        {
            return View();
        }

    }
}

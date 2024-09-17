using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.ViewComponents
{
    public class LandingPageArticlesComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}

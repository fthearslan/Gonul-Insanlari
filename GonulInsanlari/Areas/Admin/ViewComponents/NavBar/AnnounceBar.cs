using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class AnnounceBar:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();

        }
    }
}

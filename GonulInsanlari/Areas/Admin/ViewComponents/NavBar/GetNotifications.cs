using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class GetNotifications:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

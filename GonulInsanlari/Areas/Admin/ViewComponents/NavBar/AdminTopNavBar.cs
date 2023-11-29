using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class AdminTopNavBar :ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

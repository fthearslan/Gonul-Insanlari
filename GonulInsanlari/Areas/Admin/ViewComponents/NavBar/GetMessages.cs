using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class GetMessages:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

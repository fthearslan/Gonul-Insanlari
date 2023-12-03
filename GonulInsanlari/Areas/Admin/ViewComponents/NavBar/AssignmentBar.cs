using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class AssignmentBar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}

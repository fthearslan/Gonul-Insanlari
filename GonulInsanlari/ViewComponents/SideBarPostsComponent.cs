using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.ViewComponents
{
    public class SideBarPostsComponent:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}

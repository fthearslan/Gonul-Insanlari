using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.ViewComponents
{
    public class SideBarComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }


    }
}

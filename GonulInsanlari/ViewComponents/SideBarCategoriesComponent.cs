using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.ViewComponents
{
    public class SideBarCategoriesComponent:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }



    }
}

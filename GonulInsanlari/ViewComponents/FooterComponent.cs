using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.ViewComponents
{
    public class FooterComponent:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}

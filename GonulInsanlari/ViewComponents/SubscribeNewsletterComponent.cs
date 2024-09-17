using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.ViewComponents
{
    public class SubscribeNewsletterComponent:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}

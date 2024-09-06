using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using DataAccessLayer.Concrete.Providers;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class DashboardStatictic:ViewComponent
    {
       

      
        public IViewComponentResult Invoke()
        {
            using var c = new Context();


            ViewData["CommentCount"] = c.Comments
                .Where(x => x.Status == true)
                .Count();

            ViewData["ArticleCount"] = c.Articles
                .Where(x=>x.Status==true && x.IsDraft==false)
                .Count();

            ViewData["SubscriberCount"] = c.NewsLetters.
                Where(x=>x.Status == true && x.SubscriberStatus== EntityLayer.Concrete.Entities.SubscriberStatus.Active).
                Count();

            return View();
        }
    }
}

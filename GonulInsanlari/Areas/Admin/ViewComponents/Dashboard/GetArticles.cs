using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class GetArticles:ViewComponent
    {
        ArticleManager articleManager= new ArticleManager(new EFArticleDAL());
        public IViewComponentResult Invoke()
        {
            var articles = articleManager.ListReleased().Take(10).ToList();
            return View(articles);
        }
    }
}

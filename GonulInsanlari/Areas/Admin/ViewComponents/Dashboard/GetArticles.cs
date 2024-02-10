using BussinessLayer.Abstract;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class GetArticles:ViewComponent
    {
        private readonly IArticleService _manager;

        public GetArticles(IArticleService manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke()
        {
            var articles = _manager.ListReleased().Take(10).ToList();
            return View(articles);
        }
    }
}

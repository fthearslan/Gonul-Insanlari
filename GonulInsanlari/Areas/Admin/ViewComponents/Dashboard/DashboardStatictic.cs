using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class DashboardStatictic:ViewComponent
    {
        private readonly IContactService _commentManager;
        private readonly IArticleService _articleManager;

        public DashboardStatictic(IContactService commentManager, IArticleService articleManager)
        {
            _commentManager = commentManager;
            _articleManager = articleManager;
        }

        public IViewComponentResult Invoke()
        {
            var commentcount = _commentManager.List().Count;
            ViewData["CommentCount"] = commentcount;
            var articlecount = _articleManager.ListFilter().Count();
            ViewData["ArticleCount"] = articlecount;
            return View();
        }
    }
}

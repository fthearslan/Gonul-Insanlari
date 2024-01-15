using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class DashboardStatictic:ViewComponent
    {
        CommentManager commentManager = new CommentManager(new EFCommentDAL());
        ArticleManager articleManager= new ArticleManager(new EFArticleDAL());
        //VideoManager videoManager = new VideoManager(new EFVideoDAL());
        public IViewComponentResult Invoke()
        {
            var commentcount = commentManager.List().Count;
            ViewBag.CommentCount = commentcount;
            var articlecount = articleManager.ListFilter().Count();
            ViewBag.ArticleCount = articlecount;
            //var videocount = videoManager.ListFilter().Count();
            //ViewBag.VideoCount = videocount;
            return View();
        }
    }
}

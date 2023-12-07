using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class GetComments : ViewComponent
    {
        CommentManager manager = new CommentManager(new EFCommentDAL());
        public IViewComponentResult Invoke()
        {
            var comments = manager.ListFilter().Take(5).ToList();
            var count = manager.ListFilter().Count;
            ViewBag.Count = count;
            return View(comments);

        }
    }
}

using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Article
{
    public class GetCommentsByArticle : ViewComponent
    {
        CommentManager manager = new CommentManager(new EFCommentDAL());
        public IViewComponentResult Invoke(int id)
        {
            var comments = manager.GetByArticle(id);
            if (comments.Count==0)
            {
                TempData["Warning"] = "There is no comment for this article.";
            }
            return View(comments);

        }
    }
}

using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Article
{
    public class GetCommentsByArticle : ViewComponent
    {
        private readonly ICommentService _manager;

        public GetCommentsByArticle(ICommentService manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke(int id)
        {
            var comments = _manager.GetByArticle(id);
            if (comments?.Count==0)
            {
                TempData["Warning"] = "There is no comment for this article.";
            }
            return View(comments);

        }
    }
}

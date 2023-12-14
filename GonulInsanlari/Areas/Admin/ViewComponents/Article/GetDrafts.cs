using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Article
{
    public class GetDrafts:ViewComponent
    {
        ArticleManager manager = new ArticleManager(new EFArticleDAL());
        public IViewComponentResult Invoke(int pageNumber=1)
        {
           var drafts = manager.GetDrafts().ToPagedList(pageNumber,4);
            return View(drafts);
        }
    }
}

using BussinessLayer.Abstract.Services;
using Microsoft.AspNetCore.Mvc;
using ViewModelLayer.ViewModels.Article;

namespace GonulInsanlari.ViewComponents
{
    public class SideBarPostsComponent : ViewComponent
    {

        private readonly IArticleService articleManager;

        public SideBarPostsComponent(IArticleService articleManager)
        {
            this.articleManager = articleManager;
        }

        public IViewComponentResult Invoke()
        {

            List<ArticleSideBarViewModel> model = articleManager.GetWhere(a => a.Status == true && a.IsDraft == false)
             .OrderByDescending(a => a.Created)
             .Select(a => new ArticleSideBarViewModel()
             {
                 Id = a.Id, 
                 Title = a.Title,
                 CategoryName = a.Category.Name,
                 ImagePath= a.ImagePath,
                 Created = a.Created    

             }).Take(5)
             .ToList();


            return View(model);


        }

    }
}

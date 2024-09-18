using BussinessLayer.Abstract.Services;
using Microsoft.AspNetCore.Mvc;
using ViewModelLayer.ViewModels.Article;

namespace GonulInsanlari.ViewComponents
{
    public class BannerComponent : ViewComponent
    {
        private readonly IArticleService articleManager;
        public BannerComponent(IArticleService articleManager)
        {
            this.articleManager = articleManager;
        }
        public IViewComponentResult Invoke()
        {

            List<ArticleBannerViewModel> model = articleManager.GetWhere(a => a.Status == true && a.IsDraft == false)
           .OrderByDescending(a => a.Created)
           .Select(a => new ArticleBannerViewModel()
           {
               Title = a.Title,
               CategoryName = a.Category.Name,
               Description = a.Content,
               ImagePath = a.ImagePath,
               SeenCount = a.SeenCount,
               Created = a.Created

           }).Take(3)
           .ToList();

            return View(model);

        }

    }
}

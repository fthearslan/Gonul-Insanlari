using BussinessLayer.Abstract.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ViewModelLayer.ViewModels.Article;
using ViewModelLayer.ViewModels.Footer;

namespace GonulInsanlari.ViewComponents
{
    public class FooterComponent : ViewComponent
    {
        private readonly ICategoryService categoryManager;


        private readonly IArticleService articleManager;

        public FooterComponent(ICategoryService categoryManager, IArticleService articleManager)
        {
            this.categoryManager = categoryManager;
            this.articleManager = articleManager;
        }

        public IViewComponentResult Invoke()
        {

            FooterViewModel model = new();

            model.Articles = articleManager.GetWhere(x => x.Status == true && x.IsDraft == false)
                .OrderByDescending(x => x.Created)
                .Select(x => new ArticleSideBarViewModel()
                {
                    Id=x.Id,
                    Title = x.Title,
                    CategoryName = x.Category.Name,
                    Created = x.Created,
                    ImagePath = x.ImagePath,
                }).Take(3)
                .ToList();


            model.Categories = categoryManager.GetCategoriesWithArticleCount(5)
                .Select(x => new FooterCategoryViewModel()
                {

                    Name = x.Name,
                    Id = x.Id

                })
                .ToList();



            return View(model);

        }

    }
}

using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModelLayer.ViewModels.Category;
using X.PagedList;

namespace GonulInsanlari.ViewComponents
{
    public class SideBarCategoriesComponent : ViewComponent
    {

        private readonly ICategoryService categoryService;

        public SideBarCategoriesComponent(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {

            List<Category> categories = categoryService.GetCategoriesWithArticleCount(10);

            List<CategoryWithCountViewModel> model = new();

            categories?.ForEach(x =>
            {

                model.Add(new() { Id = x.Id, Name = x.Name, ArticleCount = x.Articles.Count });

            });

            return View(model);


        }



    }
}

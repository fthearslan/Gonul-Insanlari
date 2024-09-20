using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModelLayer.ViewModels.Category;
using X.PagedList;

namespace GonulInsanlari.Controllers
{
    [AllowAnonymous]
    [Route("category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryManager;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryManager, IMapper mapper)
        {
            _categoryManager = categoryManager;
            _mapper = mapper;
        }

        [Route("all")]
        public async Task<IActionResult> All(int pageNumber =1)
        {

            IPagedList<CategoryListUIViewModel> categories = await _categoryManager.GetWhere(x => x.Status == true)
                .Include(x => x.Articles)
                .Select(x => new CategoryListUIViewModel()
                {
                    ArticleCount = x.Articles.Count,
                    Created = x.Created,
                    Description = x.Description,
                    Name = x.Name,
                    Id = x.Id,
                    ImagePath = x.ImagePath,

                })
                .OrderByDescending(x => x.Created)
                .OrderByDescending(x => x.ArticleCount)
                .ToPagedListAsync(pageNumber,10);

           

            return View(categories);

        }

        [Route("details/{categoryName}")]
        public async Task<IActionResult> GetDetails(string categoryName,int pageNumber = 1)
        {

            Category? category = await _categoryManager.GetByNameAsync(categoryName);

            if (category is null)
                return NotFound();

            CategoryDetailsUIViewModel model = _mapper.Map<CategoryDetailsUIViewModel>(category);

            model.PagedArticles = await model.Articles.ToPagedListAsync(pageNumber, 10);

            return View(model);


        }





    }
}

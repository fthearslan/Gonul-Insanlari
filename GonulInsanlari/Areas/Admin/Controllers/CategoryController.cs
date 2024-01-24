using AutoMapper;
using BussinessLayer.Concrete;
using BussinessLayer.Concrete.Validations;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using FluentValidation.Results;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Security.Policy;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        CategoryManager _manager = new CategoryManager(new EFCategoryDAL());
        CategoryValidator validator = new CategoryValidator();

        public CategoryController(IMapper Mapper,ILogger<CategoryController> logger)
        {
            _mapper = Mapper;
            _logger= logger;
        }
        public IActionResult List()
        {
            List<CategoryListViewModel> model = _mapper.Map<List<CategoryListViewModel>>(_manager.GetCategoriesWithArticle());
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var category = _manager.GetById(id);
            if (category is not null)
            {
                if (category.Status == true)
                {
                    category.Status = false;
                    _manager.Update(category);
                    return RedirectToAction("List");
                }

                if (category.Status == false)
                    _manager.Delete(category);
                return RedirectToAction("List");

            }
            return RedirectToAction("List");
        }


        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(CategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                await model.SetImagePath();
                Category category = _mapper.Map<Category>(model);
                if (category != null)
                {
                    var result = await validator.ValidateAsync(category);
                    if (result.IsValid)
                    {
                        _manager.Update(category);
                        return RedirectToAction("List"); // GetDetails page is going to be placed here.
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                        }

                    }
                    return View(model);

                }

            }
            return View(model);

        }

       
        [HttpGet]
        public IActionResult EditCategory(int id)
        {

            var category = _manager.GetById(id);
            if (category != null)
            {


                try
                {
                    var model = _mapper.Map<CategoryEditViewModel>(category);
                    return View(model);
                }
                catch (AutoMapperMappingException)
                {
                    _logger.LogError($"AutoMapping Exception has been thrown, please control {category.GetType()} profile.");
                    return RedirectToAction("List"); // Error page will be placed here.
                }
                
            }
            return RedirectToAction("List");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory()
        {



            return View();
        }





        public IActionResult GetDetails(int id)
        {

            var category = _manager.GetById(id);

            if (category is not null)
            {
                return View(category);
            }

            return RedirectToAction("List");

        }

    }
}

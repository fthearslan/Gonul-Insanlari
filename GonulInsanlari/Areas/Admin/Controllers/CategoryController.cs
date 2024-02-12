using AutoMapper;
using BussinessLayer.Abstract;
using BussinessLayer.Concrete;
using BussinessLayer.Concrete.Validations;
using DataAccessLayer.Concrete.
    
    
    EntityFramework;
using EntityLayer.Entities;
using FluentValidation;
using FluentValidation.Results;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Security.Policy;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area(nameof(Admin))]
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _manager;
        private readonly AbstractValidator<Category> _validator;

        public CategoryController(IMapper Mapper, ILogger<CategoryController> logger, ICategoryService service, AbstractValidator<Category> validator)
        {
            _mapper = Mapper;
            _logger = logger;
            _manager = service;
            _validator = validator;
        }
        public IActionResult List()
        {
            List<CategoryListViewModel> model = _mapper.Map<List<CategoryListViewModel>>(_manager.GetCategoriesWithArticle());
            return View(model);
        }

        public async  Task<IActionResult> Delete(int id)
        {
            var category = await _manager.GetByIdAsync(id);
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

                try
                {
                    Category category = _mapper.Map<Category>(model);
                    if (category != null)
                    {
                        var result = await _validator.ValidateAsync(category);
                        if (result.IsValid)
                        {
                           await _manager.AddAsync(category);
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
                catch (AutoMapperMappingException)
                {
                    _logger.LogError($"AutoMapping Exception has been thrown, please control Category profile.");
                    return RedirectToAction("List"); // Error page will be placed here.
                }


            }
            return View(model);

        }


        [HttpGet]
        public async  Task<IActionResult> EditCategory(int id)
        {
            var category = await _manager.GetByIdAsync(id);
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
        public async Task<IActionResult> EditCategory(CategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.Image is not null)
                {
                    await model.SetImagePath();
                }

                try
                {
                    Category category = _mapper.Map<Category>(model);

                    var result = _validator.Validate(category);
                    if (result.IsValid)
                    {
                        _manager.Update(category);
                        return RedirectToAction("List");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                        }
                    }
                }
                catch (AutoMapperMappingException)
                {
                    _logger.LogError($"AutoMapping Exception has been thrown, please control Category profile.");

                    return RedirectToAction("List"); // Error page will be placed here.
                }

            }
            return View(model);
        }

        public IActionResult GetDetails(int id)
        {

            var category = _manager.GetDetails(id);
            if (category != null)
            {
                try
                {
                    CategoryDetailViewModel model = _mapper.Map<CategoryDetailViewModel>(category);
                    return View(model);
                }
                catch (AutoMapperMappingException)
                {
                    _logger.LogError($"AutoMapping Exception has been thrown, please control {category.GetType()} profile.");
                    return RedirectToAction("List");
                }

            }


            return RedirectToAction("List");

        }

    }
}

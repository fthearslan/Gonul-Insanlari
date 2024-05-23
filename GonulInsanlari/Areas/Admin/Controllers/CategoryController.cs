using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using BussinessLayer.Concrete.Validations;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
using FluentValidation;
using FluentValidation.Results;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol;
using System.Security.Policy;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area(nameof(Admin))]
    [Route("categories")]
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

        [Route("list")]
        public IActionResult List()
        {
            List<CategoryListViewModel> model
           = _mapper.Map<List<CategoryListViewModel>>(_manager.GetCategoriesWithArticle());
         
            return View(model);
        }

        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _manager.GetByIdAsync(id);
            if (category is not null)
                switch (category.Status)
                {
                    case true:
                        category.Status = false;
                        _manager.Update(category);
                        return RedirectToAction(nameof(List));

                    case false:
                        _manager.Delete(category);
                        return RedirectToAction(nameof(List));

                }

            return BadRequest();
           
        }


        [HttpGet]
        [Route("add")]

        public IActionResult AddCategory()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("add")]

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
                            return RedirectToAction("GetDetails", new {id=category.Id});
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
                catch (AutoMapperMappingException ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();

                }


            }
            return View(model);

        }

        [Route("edit/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _manager.GetByIdAsync(id);

            if (category != null)
                try
                {
                    var model = _mapper.Map<CategoryEditViewModel>(category);
                    return View(model);
                }
                catch (AutoMapperMappingException ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }

            return NotFound();

        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(CategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.Image is not null)
                    await model.SetImagePath();

                Category category = new();

                try
                {
                    category = _mapper.Map<Category>(model);
                }
                catch (AutoMapperMappingException ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }

                var result = _validator.Validate(category);
                if (result.IsValid)
                {
                    _manager.Update(category);
                    return RedirectToAction("GetDetails", category.Id);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
            }
            return View(model);
        }
        [Route("details/{id}")]
        public IActionResult GetDetails(int id)
        {
            var category = _manager.GetDetails(id);

            if (category != null)
                try
                {
                    CategoryDetailViewModel model = _mapper.Map<CategoryDetailViewModel>(category);
                    return View(model);
                }
                catch (AutoMapperMappingException ex)
                {

                    _logger.LogError(ex.Message);
                    return BadRequest();

                }

            return NotFound();

        }

    }
}

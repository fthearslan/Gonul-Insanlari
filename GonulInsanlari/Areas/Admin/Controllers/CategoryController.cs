using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using BussinessLayer.Concrete.Validations;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
using FluentValidation;
using FluentValidation.Results;
using GonulInsanlari.Areas.Admin.Authorization;
using GonulInsanlari.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol;
using System.Security.Policy;
using ViewModelLayer.ViewModels.Category;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route("categories")]
    public class CategoryController : Controller
    {

        #region DI Services

        private readonly IMapper _mapper;
        private readonly ICategoryService _manager;

        public CategoryController(IMapper Mapper, ICategoryService service)
        {
            _mapper = Mapper;
            _manager = service;
        }


        #endregion

        #region CREATE


        [HttpGet]
        [Route("add")]
        public IActionResult AddCategory()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("add")]
        [HasPermission(PermissionType.Category,Permission.Create)]
        public async Task<IActionResult> AddCategory(CategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await model.SetImagePath();

                Category category = _mapper.Map<Category>(model);

                await _manager.AddAsync(category);

                return RedirectToAction("GetDetails", new { id = category.Id });

            }

            return View(model);

        }

        #endregion

        #region READ

        [Route("list")]
        [HasPermission(PermissionType.Category, Permission.Read)]

        public IActionResult List()
        {

            List<CategoryListViewModel> model
           = _mapper.Map<List<CategoryListViewModel>>(_manager.GetCategoriesWithArticle());

            return View(model);
        }

        [Route("details/{id}")]
        [HasPermission(PermissionType.Category, Permission.Read)]
        public IActionResult GetDetails(int id)
        {
            var category = _manager.GetDetails(id);

            if (category != null)
            {
                CategoryDetailViewModel model = _mapper.Map<CategoryDetailViewModel>(category);
                return View(model);

            }

            return NotFound();

        }


        #endregion


        #region UPDATE


        [Route("edit/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _manager.GetByIdAsync(id);

            if (category != null)
            {
                var model = _mapper.Map<CategoryEditViewModel>(category);
                return View(model);

            }
            return NotFound();

        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        [HasPermission(PermissionType.Category,Permission.Update)]
        public async Task<IActionResult> EditCategory(CategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                    await model.SetImagePath();

                Category category = _mapper.Map<Category>(model);

                _manager.Update(category);

                return RedirectToAction("GetDetails", new { id = category.Id });

            }

            return View(model);
        }


        #endregion


        #region DELETE


        [Route("delete/{id}")]
        [HasPermission(PermissionType.Category, Permission.Delete)]
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



        #endregion


    }
}

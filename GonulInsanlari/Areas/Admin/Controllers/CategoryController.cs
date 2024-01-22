using AutoMapper;
using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {
        IMapper _mapper;

        public CategoryController(IMapper Mapper)
        {
            _mapper = Mapper;            
        }

        CategoryManager _manager = new CategoryManager(new EFCategoryDAL());
        public IActionResult List()
        {
            List<CategoryListViewModel> model = _mapper.Map<List<CategoryListViewModel>>(_manager.GetCategoriesWithArticle()); 
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var category = _manager.GetById(id);
            if(category is not null)
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

    }
}

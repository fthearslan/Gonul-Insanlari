using BussinessLayer.Concrete;
using BussinessLayer.Concrete.Validations;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using FluentValidation.Results;
using GonulInsanlari.Models;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using System.Security.Cryptography.X509Certificates;
using X.PagedList;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area("Admin")]
    [AllowAnonymous]
    public class ArticleController : Controller
    {
        UserManager<AppUser> _userManager;
        ArticleManager _articleManager = new ArticleManager(new EFArticleDAL());
        CategoryManager _categoryManager = new CategoryManager(new EFCategoryDAL());
        ArticleValidator validator = new ArticleValidator();

        public ArticleController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult List(int pageNumber = 1)
        {
            var articles = _articleManager.ListWithCategory().ToPagedList(pageNumber, 12);
            return View(articles);
        }
        [HttpGet("{Value}")]
        public IActionResult GetDetailsByNotification([FromRoute] int? value)
        {
            if (value != null)
            {
                int id = (int)value;
                var article = _articleManager.GetWithVideos(id);
                return View(article);
            }
            else
            {
                return RedirectToAction("List");
            }
        }

        public IActionResult GetDetails(int id)
        {
            var article = _articleManager.GetWithVideos(id);
            if (article != null)
            {
                return View(article);
            }
            else
            {
                return RedirectToAction("Index");

            }
        }

        [HttpGet]
        public IActionResult AddArticle()
        {
            List<SelectListItem> categories = (from x in _categoryManager.ListFilter()
                                               select new SelectListItem
                                               {
                                                   Value = x.CategoryID.ToString(),
                                                   Text = x.Name,
                                               }).ToList();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddArticle(Article article,IFormFile file,string url)
        {
            List<SelectListItem> categories = (from x in _categoryManager.ListFilter()
                                               select new SelectListItem
                                               {
                                                   Value = x.CategoryID.ToString(),
                                                   Text = x.Name,
                                               }).ToList();
            ViewBag.Categories = categories;


            ValidationResult result = validator.Validate(article);
            if (result.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (file != null)
                {
                    article.ImagePath = ImageUpload.Upload(file);
                }
                else
                {
                    
                    TempData["Error"] = "Please, select an image file.";
                    return View(article);
                }
                article.AppUser = user;
                article.Status = true;
                article.Created = DateTime.Now;
                _articleManager.Add(article);
                return RedirectToAction("List");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(article);
            }



        }

     

    }
}

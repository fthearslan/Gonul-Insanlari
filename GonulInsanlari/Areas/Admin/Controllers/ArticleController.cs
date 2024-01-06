using BussinessLayer.Concrete;
using BussinessLayer.Concrete.Validations;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using FluentValidation.Results;
using GonulInsanlari.Areas.Admin.Models;
using GonulInsanlari.Models;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Plugins;
using System.Security.Cryptography.X509Certificates;
using X.PagedList;
using Rotativa.AspNetCore;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Migrations;
using Microsoft.Extensions.Options;
using System.Security;
using FluentValidation;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;


namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area("Admin")]
    [AllowAnonymous]
    public class ArticleController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<AppUser> _userManager;
        ArticleManager _articleManager = new ArticleManager(new EFArticleDAL());
        VideoManager _videoManager = new VideoManager(new EFVideoDAL());
        CategoryManager _categoryManager = new CategoryManager(new EFCategoryDAL());
        ArticleValidator validator = new ArticleValidator();

        public ArticleController(UserManager<AppUser> userManager, IMemoryCache memoryCache)
        {
            this._userManager = userManager;
            this._memoryCache = memoryCache;
        }


        public IActionResult List(int pageNumber = 1)
        {
            var articles = _articleManager.ListReleased().ToPagedList(pageNumber, 12);
            return View(articles);
        }
        [HttpGet("{Value}")]
        public IActionResult GetDetailsByNotification([FromRoute] int? value)
        {

            if (value != null)
            {

                int id = (int)value;
                var article = _articleManager.GetWithVideos(id);
                if (article != null)
                {
                    return View(article);
                }
                else
                {
                    // Not found page will be placed here.
                    return RedirectToAction("Index");

                }

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
                // Not found page will be placed here.
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
            _memoryCache.Remove("Categories");
            _memoryCache.Set("Categories", categories);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddArticle(Article article, IFormFile image, bool? isDraft)
        {
            ViewBag.Categories = _memoryCache.Get("Categories");
            var user = await _userManager.GetUserAsync(HttpContext.User);
            article.AppUser = user;
            article.Status = true;
            article.Created = DateTime.Now;
            article.ImagePath = await ImageUpload.UploadAsync(image);

            ValidationResult result;
            if (isDraft != true)
            {
                result = validator.Validate(article);
            }
            else
            {
                result = validator.Validate(article, options => options.IncludeRuleSets("Draft"));
                article.IsDraft = true;
            }

            if (result.IsValid)
            {
                _articleManager.Add(article);
                return RedirectToAction("GetDetails", new { id = article.ArticleID });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            return View(article);
        }

        [HttpGet]
        public async Task<IActionResult> GetDrafts()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var drafts = _articleManager.GetDraftsByUser(user.Id);
            return View(drafts);

        }

        [HttpGet]
        public IActionResult GetAllAsList()
        {
            var article = _articleManager.GetAll();
            return View(article);
        }

        public IActionResult Delete(int id)
        {
            var article = _articleManager.GetById(id);
            if (article != null)
            {
                if (article.IsDraft == true)
                {
                    _articleManager.Delete(article);
                    return RedirectToAction("GetDrafts");

                }
                else
                {
                    article.Status = false;
                    _articleManager.Update(article);
                }

            }
            return RedirectToAction("GetAllAsList");

        }

        [HttpGet]
        public IActionResult EditArticle(int id)
        {
            List<SelectListItem> categories = (from x in _categoryManager.ListFilter()
                                               select new SelectListItem
                                               {
                                                   Value = x.CategoryID.ToString(),
                                                   Text = x.Name
                                               }).ToList();

            ViewBag.Categories = categories;
            TempData["Categories"] = JsonConvert.SerializeObject(categories);


            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArticle(Article article, string userId)
        {

            return View(article);

        }




    }

}

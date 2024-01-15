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
using GonulInsanlari.Areas.Admin.Models.ViewModels.Article;
using AutoMapper;
using JetBrains.Annotations;
using Humanizer;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area("Admin")]
    [AllowAnonymous]
    public class ArticleController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        ArticleManager _articleManager = new ArticleManager(new EFArticleDAL());
        CategoryManager _categoryManager = new CategoryManager(new EFCategoryDAL());
        ArticleValidator validator = new ArticleValidator();
        Context db = new Context();
        public ArticleController(UserManager<AppUser> userManager, IMemoryCache memoryCache, IMapper mapper)
        {
            this._userManager = userManager;
            this._memoryCache = memoryCache;
            this._mapper = mapper;
        }


        public async Task<IActionResult> List(int pageNumber = 1)
        {
            var articles = _articleManager.ListReleased();
            List<ArticleListViewModel> model = _mapper.Map<List<ArticleListViewModel>>(articles);
            return View(await model.ToPagedListAsync(pageNumber, 12));
        }
        [HttpGet("{Value}")]
        public IActionResult GetDetailsByNotification([FromRoute] int? value)
        {

            if (value != null)
            {

                int id = (int)value;
                var article = _articleManager.GetDetailsByUser(id);
                if (article != null)
                {
                    return View(article);
                }
                else
                {
                    return View("List");

                }
            }

            else
            {
                return View("List");
            }

        }

        public IActionResult GetDetails(int id)
        {
            var article = _articleManager.GetDetailsByUser(id);
            if (article is not null)
            {
                ArticleDetailsViewModel model = _mapper.Map<ArticleDetailsViewModel>(article);
                return View(model);
            }
            // Not found page will be placed here.
            return View();

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
            ViewData["Categories"] = categories;
            _memoryCache.Remove("Categories");
            _memoryCache.Set("Categories", categories);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddArticle(ArticleCreateViewModel model)
        {
            
            ViewData["Categories"] = _memoryCache.Get("Categories");
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                Article article = _mapper.Map<Article>(model);
                article.ImagePath = await ImageUpload.UploadAsync(model.ImagePath);
                article.Video = new Video
                {
                    Path = "Example_Path",

                };
                var result = validator.Validate(article);
                if (result.IsValid)
                {
                    await _articleManager.InserWithVideo(article);
                    _memoryCache.Remove("Categories");
                    return RedirectToAction("GetDetails", new { id = article.ArticleID });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return View(model);
                }
            }
            else
            {
                return View(model);

            }
        }



        [HttpGet]

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

            Article article = _articleManager.GetByIdInclude(id);
            ArticleEditViewModel model = _mapper.Map<ArticleEditViewModel>(article);

            ViewData["Categories"] = categories;
            _memoryCache.Set("Categories", categories);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArticle(ArticleEditViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["Categories"] = _memoryCache.Get("Categories");
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    model.ImagePath = await ImageUpload.UploadAsync(model.Image);
                }
                Article article = _mapper.Map<Article>(model);
                var result = validator.Validate(article);
                if (result.IsValid)
                {
                    article.EditedBy = user.UserName.ToString();
                    article.Status = true;
                    _articleManager.Update(article);
                    return RedirectToAction("GetDetails", new { id = article.ArticleID });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }




    }

}

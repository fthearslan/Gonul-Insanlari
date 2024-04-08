
using BussinessLayer.Concrete;
using BussinessLayer.Concrete.Validations;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
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
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area(nameof(Admin))]
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<ArticleController> _logger;
        private readonly IArticleService _articleManager;
        private readonly ICategoryService _categoryManager;
        private readonly AbstractValidator<Article> _validator;
        public ArticleController(UserManager<AppUser> userManager, IMemoryCache memoryCache, IMapper mapper, ILogger<ArticleController> logger, IArticleService articleManager, ICategoryService categoryManager, AbstractValidator<Article> validator)
        {
            this._userManager = userManager;
            this._memoryCache = memoryCache;
            this._mapper = mapper;
            _logger = logger;
            _articleManager = articleManager;
            _categoryManager = categoryManager;
            _validator = validator;
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
                try
                {
                    ArticleDetailsViewModel model = _mapper.Map<ArticleDetailsViewModel>(article);
                    return View(model);

                }
                catch (AutoMapperMappingException)
                {
                    _logger.LogError("AutoMapper exception has been thrown at GetDetails on ArticleController.");
                    return BadRequest();
                }

            return NotFound();
        }

        [HttpGet]
        public IActionResult AddArticle()
        {
            List<SelectListItem> categories = (from x in _categoryManager.ListFilter()
                                               select new SelectListItem
                                               {
                                                   Value = x.Id.ToString(),
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
                model.GetVideoUrl(model.VideoPath);
                try
                {
                    Article article = _mapper.Map<Article>(model);
                    article.AppUserID = user.Id;


                    var result = _validator.Validate(article);
                    if (result.IsValid)
                    {
                        await _articleManager.AddAsync(article);
                        _memoryCache.Remove("Categories");
                        return RedirectToAction("GetDetails", new { id = article.Id });
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
                catch (AutoMapperMappingException)
                {
                    _logger.LogError("AutoMapper exception has been thrown at AddArticle on ArticleController.");
                    return View(nameof(List));
                }



            }
            else
            {
                return View(model);

            }
        }

        [HttpGet]

        public async Task<ActionResult> Delete(int id)
        {
            var article = await _articleManager.GetByIdAsync(id);
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
            Article article = _articleManager.GetByIdInclude(id);
           
            if(article is null)
                return NotFound();

            List<SelectListItem> categories = (from x in _categoryManager.ListFilter()
                                               select new SelectListItem
                                               {
                                                   Value = x.Id.ToString(),
                                                   Text = x.Name
                                               }).ToList();

            ViewData["Categories"] = categories;
            _memoryCache.Set("Categories", categories);

            try
            {
                ArticleEditViewModel model = _mapper.Map<ArticleEditViewModel>(article);
                return View(model);
            }
            catch (AutoMapperMappingException)
            {
                _logger.LogError("Mapping exception has been thrown while executing [GET] EditArticle() in ArticleController.");
                return BadRequest();
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArticle(ArticleEditViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            ViewData["Categories"] = _memoryCache.Get("Categories");

            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                    await model.GetImage(model.Image);
                model.GetVideoUrl(model.VideoPath);
                try
                {
                    Article article = _mapper.Map<Article>(model);

                    var result = _validator.Validate(article);
                    if (result.IsValid)
                    {
                        article.EditedBy = user.UserName;
                        _articleManager.Update(article);

                        return RedirectToAction("GetDetails", new { id = article.Id });
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
                catch (AutoMapperMappingException)
                {
                    _logger.LogError("Mapping exception has been thrown while executing [POST] EditArticle() in ArticleController.");
                    return View(model);
                }

            }

            return View(model);

        }




    }

}

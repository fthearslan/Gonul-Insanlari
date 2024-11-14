using BussinessLayer.Concrete;
using BussinessLayer.Concrete.Validations;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using FluentValidation.Results;
using GonulInsanlari.Models;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
using AutoMapper;
using JetBrains.Annotations;
using Humanizer;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.ViewModels.Article;
using BussinessLayer.Concrete.Validations.FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GonulInsanlari.Areas.Admin.Authorization;
using GonulInsanlari.Enums;
using Quartz.Impl.Calendar;
using ViewModelLayer.Models.Newsletter;
using ViewModelLayer.Models.Tools;
using Microsoft.AspNetCore.SignalR;
using GonulInsanlari.Hubs;
using ViewModelLayer.Models.Notification;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area(nameof(Admin))]
    [Route("articles")]
    [Authorize]
    public class ArticleController : Controller
    {

        #region DI Services

        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IArticleService _articleManager;
        private readonly ICategoryService _categoryManager;
        private readonly IEmailService _emailManager;

        public ArticleController(UserManager<AppUser> userManager, IMemoryCache memoryCache, IMapper mapper, ILogger<ArticleController> logger, IArticleService articleManager, ICategoryService categoryManager, IEmailService emailManager)
        {
            this._userManager = userManager;
            this._memoryCache = memoryCache;
            this._mapper = mapper;
            _articleManager = articleManager;
            _categoryManager = categoryManager;
            _emailManager = emailManager;
        }


        #endregion


        #region Create


        [Route("add")]
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



        [Route("add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasPermission(PermissionType.Article, Permission.Create)]
        public async Task<IActionResult> AddArticle([FromServices] IEmailService _emailManager, ArticleCreateViewModel model)
        {

            ViewData["Categories"] = _memoryCache.Get("Categories");

            var user = await _userManager.GetUserAsync(HttpContext.User);


            if (ModelState.IsValid)
            {
                Article article = _mapper.Map<Article>(model);

                article.AppUserID = user.Id;

                await _articleManager.AddAsync(article);

                await _emailManager.SendNewsletterAsync(_mapper.Map<WeeklyNewsletterModel>(article));

                _memoryCache.Remove("Categories");

                await Task.Delay(3000);

                return RedirectToAction("GetDetails", new { id = article.Id });
            }

            return View(model);

        }



        #endregion


        #region Read


        [Route("list")]
        [HasPermission(PermissionType.Article, Permission.Read)]

        public async Task<IActionResult> List(int pageNumber = 1)
        {
            var articles = _articleManager.ListReleased();

            List<ArticleListViewModel> model = _mapper.Map<List<ArticleListViewModel>>(articles);

            return View(await model.ToPagedListAsync(pageNumber, 12));
        }


        [Route("details/{id}")]
        [HasPermission(PermissionType.Article, Permission.Read)]

        public IActionResult GetDetails(int id)
        {
            var article = _articleManager.GetDetailsByUser(id);

            if (article is not null)
            {

                ViewData["articleTitle"] = article.Title;

                ArticleDetailsViewModel model = _mapper.Map<ArticleDetailsViewModel>(article);

                return View(model);
            }

            return NotFound();

        }



        #endregion


        #region Update


        [Route("edit/{id}")]
        [HttpGet]
        public IActionResult EditArticle(int id)
        {
            Article article = _articleManager.GetByIdInclude(id);

            if (article is null)
                return NotFound();

            ArticleEditViewModel model = _mapper.Map<ArticleEditViewModel>(article);

            List<SelectListItem> categories = (from x in _categoryManager.ListFilter()
                                               select new SelectListItem
                                               {
                                                   Value = x.Id.ToString(),
                                                   Text = x.Name
                                               }).ToList();

            ViewData["Categories"] = categories;
            _memoryCache.Set("Categories", categories);


            return View(model);

        }



        [Route("edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasPermission(PermissionType.Article, Permission.Update)]
        public async Task<IActionResult> EditArticle(ArticleEditViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            ViewData["Categories"] = _memoryCache.Get("Categories");

            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                    await model.GetImage(model.Image);

                model.GetVideoUrl(model.VideoPath);

                Article article = _mapper.Map<Article>(model);

                article.EditedBy = user?.UserName;



                _articleManager.Update(article);

                return RedirectToAction("GetDetails", new { id = article.Id });

            }

            return View(model);

        }


        #endregion


        #region Delete


        [Route("delete/{id:int}")]
        [HttpPost]
        [HasPermission(PermissionType.Article, Permission.Delete)]
        public async Task<ActionResult> Delete(int id)
        {
            var article = await _articleManager.GetByIdAsync(id);

            if (article is null)
                return NotFound();



            if (article.Status == true)
            {
                article.Status = false;
                _articleManager.Update(article);

            }
            else
            {
                _articleManager.Delete(article);

            }

            return StatusCode(200);


        }



        #endregion


    }

}

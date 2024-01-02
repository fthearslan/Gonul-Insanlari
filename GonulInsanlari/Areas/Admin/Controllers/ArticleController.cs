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
                    if (article.Videos != null)
                    {
                        foreach (var vid in article.Videos)
                        {
                            var video = _videoManager.GetById(vid.VideoID);
                            ViewBag.Path = video.Path;
                            ViewBag.IsUrl = video.IsUrl;
                        }
                    }
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
                if (article.Videos != null)
                {
                    foreach (var vid in article.Videos)
                    {
                        var video = _videoManager.GetById(vid.VideoID);
                        ViewBag.Path = video.Path;
                        ViewBag.IsUrl = video.IsUrl;
                    }
                }
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddArticle(Article article, IFormFile file, IFormFile video, string url, string isDraft)
        {


            List<SelectListItem> categories = (from x in _categoryManager.ListFilter()
                                               select new SelectListItem
                                               {
                                                   Value = x.CategoryID.ToString(),
                                                   Text = x.Name,
                                               }).ToList();
            ViewBag.Categories = categories;




            if (video == null || url == null)
            {
                article.ImagePath = ImageUpload.Upload(file);
                var user = await _userManager.GetUserAsync(HttpContext.User);
                article.AppUser = user;
                article.Created = DateTime.Now;
                article.Status = true;
                ValidationResult result;
                if (isDraft == "true")
                {
                    article.IsDraft = true;
                    result = await validator.ValidateAsync(article, options => options.IncludeRuleSets("Draft"));
                }
                else
                {
                    result = await validator.ValidateAsync(article);

                }
                if (result.IsValid)
                {
                    _articleManager.Add(article);

                    if (video != null)
                    {

                        article.Videos = new List<ArticleVideo>()
                 { new ArticleVideo()
                    {

                        ArticleID=article.ArticleID,
                        Video=new Video()
                        {
                            Path=ImageUpload.Upload(video),
                            IsUrl=false,
                            AppUser=user

                        }
                    }

                 };
                        _articleManager.Update(article);
                        return RedirectToAction("List");

                    }
                    else
                    {
                        if (url != null)
                        {


                            article.Videos = new List<ArticleVideo>()

                 { new ArticleVideo()
                    {

                        ArticleID=article.ArticleID,
                        Video=new Video()
                        {
                            Path=GetUrl.GetVideoUrl(url),
                            IsUrl=true,
                            AppUser=user

                        }
                    }

                 };
                            _articleManager.Update(article);

                            return RedirectToAction("GetDetails", new { id = article.ArticleID });

                        }
                        else
                        {

                            return RedirectToAction("List");
                        }

                    }
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
            else
            {
                TempData["Error"] = "Please choose either a video file or video url.";
                return View(article);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDrafts(int pageNumber = 1)
        {
            if (_memoryCache.Get("List") is not null)
            {
                var draft_list = new List<Article>();
                draft_list = _memoryCache.Get("List") as List<Article>;
                TempData["Search"] = _memoryCache.Get("Count");
                return View(await draft_list.ToPagedListAsync(pageNumber,15));
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var drafts = await _articleManager.GetDraftsByUser(user.Id).ToPagedListAsync(pageNumber, 15);
            TempData["id"] = user.Id;
            return View(drafts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetDrafts(string search, int pageNumber = 1)
        {
            int id = Convert.ToInt16(TempData["id"]);
            var drafts = _articleManager.GetDraftsByUser(id);
            if (drafts != null && search != null)
            {
                List<Article> draft_list = new List<Article>();
                foreach (var draft in drafts)
                {
                    if (draft.Title.Contains(search))
                    {
                        draft_list.Add(draft);
                    }
                }
                TempData["Search"] = draft_list.Count + " results for '"+ search + "' are being displayed.";
               const string cacheKey = "List";
                const string count = "Count";
                _memoryCache.Set(cacheKey,draft_list);
                _memoryCache.Set(count, TempData["Search"]);

                return View(await draft_list.ToPagedListAsync(pageNumber, 15));

            }
            _memoryCache.Remove("List");
            return View(await drafts.ToPagedListAsync(pageNumber, 15));
        }

        public async Task<IActionResult> GetAllAsList(int pageNumber = 1)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var article = await _articleManager.GetAll().ToPagedListAsync(pageNumber, 20);
            return View(article);
        }



    }

}

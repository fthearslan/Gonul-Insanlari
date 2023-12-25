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

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area("Admin")]
    [AllowAnonymous]
    public class ArticleController : Controller
    {
        UserManager<AppUser> _userManager;
        ArticleManager _articleManager = new ArticleManager(new EFArticleDAL());
        VideoManager _videoManager = new VideoManager(new EFVideoDAL());
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
        public async Task<IActionResult> AddArticle(Article article, IFormFile file, IFormFile video, string url)
        {
            List<SelectListItem> categories = (from x in _categoryManager.ListFilter()
                                               select new SelectListItem
                                               {
                                                   Value = x.CategoryID.ToString(),
                                                   Text = x.Name,
                                               }).ToList();
            ViewBag.Categories = categories;
            if (video==null || url==null) 
            {
                article.ImagePath = ImageUpload.Upload(file);
                var user = await _userManager.GetUserAsync(HttpContext.User);
                article.AppUser = user;
                article.Created = DateTime.Now;
                article.Status = true;
                ValidationResult result = validator.Validate(article);
                if (result.IsValid)
                {

                    if (video != null)
                    {
                        _articleManager.Add(article);
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
                            _articleManager.Add(article);

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

                            return RedirectToAction("List");

                        }
                        else
                        {
                            _articleManager.Add(article);
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
                return View();
            }
        }


    }
}

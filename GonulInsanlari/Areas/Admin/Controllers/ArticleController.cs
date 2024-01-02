﻿using BussinessLayer.Concrete;
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
                if (article.IsDraft==true)
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


    }

}

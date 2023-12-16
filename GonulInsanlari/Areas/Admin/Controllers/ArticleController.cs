using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.X509Certificates;
using X.PagedList;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area("Admin")]
    [AllowAnonymous]
    public class ArticleController : Controller
    {
        ArticleManager _articleManager = new ArticleManager(new EFArticleDAL());
        CategoryManager _categoryManager = new CategoryManager(new EFCategoryDAL());
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
                return RedirectToAction("Index");
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
        public IActionResult AddArticle(Article article)
        {
           
            return View();
        }
    }
}

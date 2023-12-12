using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area("Admin")]
    [AllowAnonymous]
    public class ArticleController : Controller
    {
        ArticleManager manager = new ArticleManager(new EFArticleDAL());
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{Value}")]
        public IActionResult GetDetailsByNotification([FromRoute] int? value)
        {



            if (value != null)
            {
                int id = (int)value;
                var article = manager.GetWithVideos(id);
                return View(article);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        public IActionResult GetDetails(int id)
        {
            var article = manager.GetWithVideos(id);
            if (article != null)
            {
                return View(article);
            }
            else
            {
                return RedirectToAction("Index");

            }

        }
    }
}

using EntityLayer;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace GonulInsanlari.Controllers
{
    public class PrintControlller : Controller
    {
        ArticleManager _articleManager = new ArticleManager(new EFArticleDAL());
        public IActionResult Index()
        {
            return View();
        }

       
    public IActionResult PrintPage(int id)
        {
            var article=_articleManager.GetById(id);

            string fileName = Guid.NewGuid().ToString() + ".pdf";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PdfReports/" + fileName);

            var stream = new FileStream(path, FileMode.Create);
            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);
            document.Open();
            document.AddTitle(article.Title);
            Paragraph content = new Paragraph(article.Content);
            document.Add(content);
            document.Close();
            return RedirectToAction("Index","Dashboard");

        }   
    }

    
}

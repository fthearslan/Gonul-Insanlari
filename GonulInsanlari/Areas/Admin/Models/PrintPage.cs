using EntityLayer;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace GonulInsanlari.Areas.Admin.Models
{
    public static class PrintPage
    {

        public static void Print(Article article)
        {
            string fileName = Guid.NewGuid().ToString() + ".pdf";
            string path =  Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PdfReports/" + fileName);

            var stream = new FileStream(path, FileMode.Create);
            Document document= new Document(PageSize.A4);
         PdfWriter.GetInstance(document,stream);
            document.Open();
            document.AddTitle(article.Title);
          Paragraph content=new Paragraph(article.Content);
            document.Add(content);
            document.Close();


        }
    }
}

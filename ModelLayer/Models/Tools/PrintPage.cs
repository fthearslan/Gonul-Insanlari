using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Reflection.Metadata;

namespace ViewModelLayer.Models.Tools
{
    public static class PrintPage
    {

        public static void Print(string title,string Content)
        {
            string fileName = Guid.NewGuid().ToString() + ".pdf";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PdfReports/" + fileName);

            var stream = new FileStream(path, FileMode.Create);
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);
            document.Open();
            document.AddTitle(title);
            Paragraph content = new Paragraph(Content);
            document.Add(content);
            document.Close();


        }
    }
}

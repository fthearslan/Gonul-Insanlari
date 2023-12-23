namespace GonulInsanlari.Models
{
    public class ImageUpload
    {
        public static string Upload(IFormFile file)
        {
            if (file != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/", file.FileName);
                if (System.IO.File.Exists(path))
                {
                    return file.FileName;
                }
                var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
                return file.FileName;
            }
            return null;
        }
    }
}

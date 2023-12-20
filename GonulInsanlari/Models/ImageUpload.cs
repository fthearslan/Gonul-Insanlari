namespace GonulInsanlari.Models
{
    public class ImageUpload
    {
        public static string Upload(IFormFile file)
        {
         if(file !=null)
            {
                string Extension = Path.GetExtension(file.FileName);
                string Image = Guid.NewGuid().ToString() + Extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/", Image);
                var stream = new FileStream(location, FileMode.Create);
                file.CopyTo(stream);
                return Image;
            }
            return null;
            
           
        }
    }
}

using EntityLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GonulInsanlari.Models
{
    public class ImageUpload
    {
        public static async Task<string> UploadAsync(IFormFile? file)
        {
            if (file != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/", file.FileName);
                if (System.IO.File.Exists(path))
                {
                    return file.FileName;
                }
                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                return file.FileName;
            }
            return null;
        }


        public static async Task<List<string>> UploadFileAsync(IList<IFormFile>? files)
        {

            if (files != null)
            {
                List<string> Paths = new();

                foreach (var file in files)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/", file.FileName);

                    if (System.IO.File.Exists(path))
                    {
                        Paths.Add(file.FileName);
                        return Paths;
                    }

                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                    Paths.Add(file.FileName);

                }
                return Paths;

            }

            return null;
        }


        public static async Task<bool> CheckFileSizeAsync(IList<IFormFile>? files)
        {
            if (files != null && files.Count > 0)
                return true;

            return false;
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;

namespace ViewModelLayer.Models.Tools
{
    public class ImageUpload
    {
        public static async Task<string> UploadAsync(IFormFile? file)
        {
            if (file != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/", file.FileName);
                if (File.Exists(path))
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

                    if (File.Exists(path))
                    {
                        Paths.Add(file.FileName);
                        continue;
                    }

                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                    Paths.Add(file.FileName);

                }
                return Paths;

            }

            return null;
        }


        public static bool CheckFileSizeAsync(IList<IFormFile>? files)
        {
            const int maxfileSize = 2 * 1024 * 1024;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {

                    if (file.Length > maxfileSize)
                        return false;
                }
                return true;
            }

            return false;

        }
    }
}

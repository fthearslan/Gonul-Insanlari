using EntityLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Net.Mail;
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
                    string fileName = file.FileName.Replace(" ", "");

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/", fileName);

                    if (File.Exists(path))
                    {
                        Paths.Add(fileName);
                        continue;
                    }

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        Paths.Add(file.FileName);

                    };

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

        public static bool CheckFileSize(IFormFile? file)
        {
            const int maxfileSize = 2 * 1024 * 1024;

            if (file != null)
            {
                if (file.Length > maxfileSize)
                    return false;

                return true;
            }

            return false;

        }


        public static bool DeleteFile(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/", fileName);

            if (!File.Exists(path))
                return false;

            System.IO.File.Delete(path);

            if (!File.Exists(path))
                return true;

            return false;

        }


    }


}

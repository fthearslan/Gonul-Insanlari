using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.RegularExpressions;

namespace ViewModelLayer.Models.Tools
{
    public static class GetString
    {

        public static string GetRawString(string text)
        {

            if (text is not null)
                return Regex.Replace(text, "<.*?>", string.Empty)
                     .Replace("&nbsp;", " ");

            return null;

        }

        public static string GetSlugUrl(string title)
        {

            title = title.ToLowerInvariant(); // Küçük harfe çevir
            title = title.Replace(" ", "-"); // Boşlukları tire ile değiştir
            title = Regex.Replace(title, @"[^a-z0-9\s-]", ""); // Geçersiz karakterleri kaldır
            title = Regex.Replace(title, @"\s+", " ").Trim(); // Birden fazla boşluğu tek boşluğa indir ve kenar boşluklarını kaldır
            title = Regex.Replace(title, @"\s", "-"); // Boşlukları tire ile değiştir

            return title;

        }


    }
}

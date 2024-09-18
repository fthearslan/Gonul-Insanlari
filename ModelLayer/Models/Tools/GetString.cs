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
                return Regex.Replace(text, "<.*?>", string.Empty);

            return null;

        }


    }
}

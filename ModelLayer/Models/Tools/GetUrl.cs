﻿using Microsoft.AspNetCore.Mvc;

namespace ViewModelLayer.Models.Tools
{
    public static class GetUrl
    {

        public static string GetVideoUrl(string? rawUrl)
        {
            if (rawUrl != null)
            {

                string[] urls = rawUrl.Split("/");

                string url = "https://www.youtube.com/embed/" + urls.Last();
                return url;
            }
            return null;
        }


     

    }
}

namespace GonulInsanlari.Areas.Admin.Models
{
    public static class GetUrl
    {

        public static string GetVideoUrl(string rawUrl)
        {

            string[] urls = rawUrl.Split("/");

            string url = "https://www.youtube.com/embed/" + urls.Last();
            return url;

        }




    }
}

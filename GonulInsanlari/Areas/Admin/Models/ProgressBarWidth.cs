namespace GonulInsanlari.Areas.Admin.Models
{
    public static class ProgressBarWidth
    {
        public static int GetWidth(DateTime Created,DateTime Due)
        {
          var days = Due.Subtract(Created).Days;
            var past = DateTime.Now.Subtract(Created).Days;
            decimal value = 100 * past / days;
          var width=(int)Math.Round(value, 0);
            return width;

        }
        
    }
}

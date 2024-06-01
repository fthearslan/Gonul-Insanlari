namespace ViewModelLayer.Models.Tools
{
    public static class GetDate
    {
        public static string GetCreateDate(DateTime date)
        {
            const double minute = 1;
            const double hour = 60 * minute;
            const double day = 24 * hour;
            const double week = 7 * day;

            double a = 0;

            TimeSpan timeSpan = DateTime.Now.Subtract(date);
            double span = timeSpan.TotalMinutes;



            if (span < minute)
            {
                return "Just now";
            }
            if (span >= minute && span < hour)
            {
                a = span / minute;
                a = Math.Round(a, 0);
                return a + " minutes ago";
            }
            if (span >= hour && span < day)
            {
                a = span / hour;
                a = Math.Round(a, 0);

                return a + " hours ago";
            }
            if (span >= day && span < week)
            {
                string DayOf = date.DayOfWeek.ToString();
                return DayOf;
            }
            if (span > day && span < day * 2)
            {
                return "Yesterday";
            }
            if (span > week)
            {
                return date.ToString("dd-MMM-yyyy");
            }

            return date.ToString("dd-MMM-yyyy");

        }
    }
}

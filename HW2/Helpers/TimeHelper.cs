namespace GaiaShare.Helpers
{
    public static class TimeHelper
    {

        // Set for 2 weeks back to show updates of farm inventory (JVP-May-2022)
        public static DateTime RecentDate()
        {
            var now = DateTime.UtcNow;
            var twoweeksago = now.AddDays(-14);
            return twoweeksago;
        }

        // https://stackoverflow.com/questions/11/calculate-relative-time-in-c-sharp
        // Date should be in UTC for best results.
        public static string getDateTimeDelta(DateTime d)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - d.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 10 * SECOND)
                return "A few seconds ago";

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }

        public static DateTime TwoWeeksAgo { get; } = DateTime.UtcNow.AddDays(-14);
        public static DateTime TwoDaysAgo { get; } = DateTime.UtcNow.AddDays(-2);
    }
}

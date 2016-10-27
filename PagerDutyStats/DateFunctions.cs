using System;

namespace PagerDutyStats
{
    public static class DateFunctions
    {
        public static DateTime NextFriday(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Friday)
            {
                date = date.AddDays(1);
            }

            return date;
        }

        public static DateRange MakeWeekendRange(DateTime startDate)
        {
            var startTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, 17, 0, 0);
            var endDate = startDate.AddDays(3);
            var endTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, 9, 0, 0);
            return new DateRange(startTime, endTime);
        }

        public static string AsIso8601ToMinute(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm");
        }

        public static string AsIso8601Date(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
    }
}

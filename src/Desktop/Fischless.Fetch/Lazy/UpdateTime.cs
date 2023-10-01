using System.Globalization;

namespace Fischless.Fetch.Lazy;

public static class UpdateTime
{
    internal const string DATETIME_FORMAT = "yyyy/MM/dd HH:mm:ss";
    public static DateTime NextUpdateTime => GetUpdateNextTime();
    public static string NextUpdateTimeViewString => NextUpdateTime.ToString(DATETIME_FORMAT);

    public static bool IsCountStartedFormNextUpdateTime { get; set; } = false;

    public static DateTime GetUpdateNextTime()
    {
        int timeZoneOffset8 = 8 - TimeZoneInfo.Local.BaseUtcOffset.Hours;
        DateTime dateTime8 = DateTime.Now.AddHours(timeZoneOffset8);
        DateTime dateTimeUpdate8 = new(dateTime8.Year, dateTime8.Month, dateTime8.Day, 4, 0, 0, DateTimeKind.Utc);

        if (dateTime8.Hour >= 4)
        {
            dateTimeUpdate8 = dateTimeUpdate8.AddDays(1);
        }
        return dateTimeUpdate8.AddHours(-timeZoneOffset8);
    }

    public static bool IsToday(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Utc)
        {
            DateTime dateTime8 = dateTime.AddHours(TimeZoneInfo.Local.BaseUtcOffset.Hours);
            TimeSpan ts = NextUpdateTime.ToUniversalTime() - dateTime8;
            return ts.TotalHours < 24d;
        }
        else
        {
            TimeSpan ts = NextUpdateTime.ToUniversalTime() - dateTime;
            return ts.TotalHours < 24d;
        }
    }

    public static string ToDateTimeString(this DateTime self, string format = DATETIME_FORMAT)
    {
        return self.ToString(format);
    }

    public static DateTime? ToDateTime(this string self, string format = DATETIME_FORMAT)
    {
        if (string.IsNullOrEmpty(self))
        {
            return null;
        }
        DateTimeFormatInfo dtFormat = new()
        {
            ShortDatePattern = format,
        };
        if (DateTime.TryParse(self, dtFormat, DateTimeStyles.None, out DateTime dateTime))
        {
            return dateTime;
        }
        return null;
    }

    public static DateTime GetSundayDate(DateTime someDate, int hour = 4)
    {
        int i = someDate.DayOfWeek - DayOfWeek.Sunday;

        if (i != 0)
        {
            i = 7 - i;
        }
        TimeSpan ts = new(i, hour, 0, 0);
        return someDate.Add(ts);
    }

    public static TimeSpan GetSundayDateOffset(int hour = 4)
    {
        DateTime now = DateTime.Now;
        return GetSundayDate(now, hour) - now;
    }

    public static DateTime Today(int hour = 10)
    {
        DateTime now = DateTime.Now;
        return new(now.Year, now.Month, now.Day, hour, 0, 0);
    }

    public static TimeSpan TodayOffset(int hour = 10)
    {
        DateTime now = DateTime.Now;
        return Today(hour) - now;
    }
}

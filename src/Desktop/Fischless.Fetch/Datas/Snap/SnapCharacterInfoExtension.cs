using System.Globalization;

namespace Fischless.Fetch.Datas.Snap;

internal static class SnapCharacterInfoExtension
{
    public static DateTime ParseExactDateTime(this string utcDateString)
    {
        DateTime utcDateTime = DateTime.ParseExact(utcDateString, "yyyy-MM-ddTHH:mm:ss.fffffffZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
        DateTime localDateTime = utcDateTime.ToLocalTime();
        return localDateTime;
    }
}

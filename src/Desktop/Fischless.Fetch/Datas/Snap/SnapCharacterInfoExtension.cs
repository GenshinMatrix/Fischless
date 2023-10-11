using System.Globalization;

namespace Fischless.Fetch.Datas.Snap;

internal static class SnapCharacterInfoExtension
{
    public static DateTime ParseExactDateTime(this string utcDateString)
    {
        DateTime utcDateTime = DateTime.Parse(utcDateString, null, DateTimeStyles.RoundtripKind);
        DateTime localDateTime = utcDateTime.ToLocalTime();
        return localDateTime;
    }
}

using System.Diagnostics;

namespace Fischless.Fetch.Lazy;

public class LazyInputHelper
{
    public static LazyInput[] AnalysisFile(string fileIn)
    {
        List<LazyInput> outputs = new();

        try
        {
            Dictionary<string, object> paramDict = LazyOutputSerializer.DeserializeObject<Dictionary<string, object>>(fileIn);

            if (paramDict != null)
            {
                if (paramDict.ContainsKey("Completed"))
                {
                    object l = paramDict["Completed"];

                    if (l is List<object> ee)
                    {
                        foreach (object eee in ee)
                        {
                            if (eee is Dictionary<object, object> eeee)
                            {
                                string uidIn = eeee[nameof(LazyOutput.Uid)]?.ToString()!;
                                string dateTimeUtcStringIn = eeee[nameof(LazyOutput.DateTimeUtc)]?.ToString()!;
                                DateTime? dateTimeUtcIn = dateTimeUtcStringIn.ToDateTime();
                                bool today = false;

                                if (dateTimeUtcIn != null)
                                {
                                    if (UpdateTime.IsToday(dateTimeUtcIn.Value.AddHours(TimeZoneInfo.Local.BaseUtcOffset.Hours).ToUniversalTime()))
                                    {
                                        today = true;
                                    }
                                }

                                outputs.Add(new()
                                {
                                    Uid = uidIn,
                                    Today = today,
                                    DateTime = dateTimeUtcIn?.AddHours(TimeZoneInfo.Local.BaseUtcOffset.Hours).ToDateTimeString() ?? dateTimeUtcStringIn,
                                });
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.ToString());
        }

        return outputs.ToArray();
    }
}

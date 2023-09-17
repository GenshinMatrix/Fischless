using Fischless.Fetch.Regedit;
using System.Diagnostics;

namespace Fischless.Fetch.Lazy;

public static class LazyOutputHelper
{
    public static readonly string Path = LazySpecialPathProvider.GetPath("genshin-lazy.yaml");

    public static async Task Save(string uid, string prod = null!, DateTime? dateTime = null!)
    {
        try
        {
            string fileIn = File.Exists(Path) ? File.ReadAllText(Path) : string.Empty;
            string fileOut = await SaveFile(fileIn, uid, prod, dateTime);
            if (fileOut == null) return;

            await File.WriteAllTextAsync(Path, fileOut);
            if (await LazyRepository.SetupToken())
            {
                string content = await LazyRepository.GetFile();

                if (content != null)
                {
                    string contentNew = await SaveFile(content, uid, prod, dateTime);
                    if (contentNew == null) return;
                    _ = await LazyRepository.UpdateFile(contentNew);
                }
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.ToString());
        }
    }

    public static async Task<string> SaveFile(string fileIn, string uid, string prod = null!, DateTime? dateTime = null!)
    {
        await Task.CompletedTask;
        try
        {
            Dictionary<string, object> paramDict = LazyOutputSerializer.DeserializeObject<Dictionary<string, object>>(fileIn);
            List<LazyOutput> outputs = null!;

            paramDict ??= new();
            if (!paramDict.ContainsKey("Completed"))
            {
                outputs = new List<LazyOutput>();
                paramDict.Add("Completed", outputs);
            }
            if (outputs is not List<LazyOutput>)
            {
                outputs = new List<LazyOutput>();
                object l = paramDict["Completed"];

                if (l is List<object> ee)
                {
                    foreach (object eee in ee)
                    {
                        if (eee is Dictionary<object, object> eeee)
                        {
                            outputs.Add(new()
                            {
                                Uid = eeee[nameof(LazyOutput.Uid)]?.ToString()!,
                                Prod = eeee[nameof(LazyOutput.Prod)]?.ToString()!,
                                DateTimeUtc = eeee[nameof(LazyOutput.DateTimeUtc)]?.ToString()!,
                            });
                        }
                    }
                }
            }

            LazyOutput found = null!;
            outputs.ForEach(output =>
            {
                if (output.Uid == uid || output.Prod == prod)
                {
                    found = output;
                }
            });

            string dateTimeString = (dateTime ?? DateTime.Now).ToUniversalTime().ToDateTimeString();
            prod ??= GIRegedit.ProdCN ?? GIRegedit.ProdOVERSEA;

            if (found is null)
            {
                outputs.Add(new()
                {
                    Uid = uid,
                    Prod = prod,
                    DateTimeUtc = dateTimeString,
                });
            }
            else
            {
                found.Uid = uid;
                found.Prod = prod;
                found.DateTimeUtc = dateTimeString;
            }

            paramDict["Completed"] = outputs;

            string fileOut = LazyOutputSerializer.SerializeObject(paramDict);
            return fileOut;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.ToString());
        }
        return null!;
    }

    public static async Task<bool> Check(string uid, string prod = null!)
    {
        try
        {
            if (File.Exists(Path))
            {
                string fileIn = await File.ReadAllTextAsync(Path);
                if (await CheckFile(fileIn, uid, prod))
                {
                    return true;
                }
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.ToString());
        }

        if (await LazyRepository.SetupToken())
        {
            string content = await LazyRepository.GetFile();

            if (await CheckFile(content, uid, prod))
            {
                return true;
            }
        }
        return false;
    }

    public static async Task<bool> CheckFile(string fileIn, string uid, string prod = null!)
    {
        await Task.CompletedTask;
        try
        {
            Dictionary<string, object> paramDict = LazyOutputSerializer.DeserializeObject<Dictionary<string, object>>(fileIn);

            uid ??= "0000000000";
            //prod ??= GenshinRegedit.ProdCN ?? GenshinRegedit.ProdOVERSEA;

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
                                string prodIn = eeee[nameof(LazyOutput.Prod)]?.ToString()!;

                                if (uidIn == uid || prodIn == prod)
                                {
                                    string dateTimeUtcStringIn = eeee[nameof(LazyOutput.DateTimeUtc)]?.ToString()!;
                                    DateTime? dateTimeUtcIn = dateTimeUtcStringIn.ToDateTime();

                                    if (dateTimeUtcIn != null)
                                    {
                                        if (UpdateTime.IsToday(dateTimeUtcIn.Value.AddHours(TimeZoneInfo.Local.BaseUtcOffset.Hours).ToUniversalTime()))
                                        {
                                            return true;
                                        }
                                    }
                                }
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
        return false;
    }
}

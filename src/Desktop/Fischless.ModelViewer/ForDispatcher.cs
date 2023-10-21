using System.IO;

namespace Fischless.ModelViewer;

public static class ForDispatcher
{
    public static string ApplicationModelPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"GenshinModelViewer\models");
}

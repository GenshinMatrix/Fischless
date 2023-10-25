namespace Fischless.Fetch.Regedit;

public sealed class GIRegeditUninstallInfo
{
    public const string SKEY_DISPLAY_ICON = "DisplayIcon";
    public const string SKEY_DISPLAY_NAME = "DisplayName";
    public const string SKEY_DISPLAY_VERSION = "DisplayVersion";
    public const string SKEY_ESTIMATED_SIZE = "EstimatedSize";
    public const string SKEY_EXE_NAME = "ExeName";
    public const string SKEY_INSTALL_PATH = "InstallPath";
    public const string SKEY_NETWORK_TYPE = "NetworkType";
    public const string SKEY_PUBLISHER = "Publisher";
    public const string SKEY_UNINSTALL_STRING = "UninstallString";
    public const string SKEY_URL_INFO_ABOUT = "URLInfoAbout";
    public const string SKEY_UUID = "UUID";

    public string RegistryKey = null!;

    public string DisplayIcon = null!;

    public string DisplayName = null!;

    public string DisplayVersion = null!;

    public object EstimatedSize = null!;

    public string ExeName = null!;

    public string InstallPath = null!;

    public string NetworkType = null!;

    public string Publisher = null!;

    public string UninstallString = null!;

    public string URLInfoAbout = null!;

    public string UUID = null!;
}

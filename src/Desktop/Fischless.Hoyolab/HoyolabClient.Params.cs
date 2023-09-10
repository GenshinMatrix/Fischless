namespace Fischless.Hoyolab;

#pragma warning disable IDE0051
public partial class HoyolabClient
{
    private const string Accept = "Accept";
    private const string Cookie = "Cookie";
    private const string UserAgent = "User-Agent";
    private const string X_Reuqest_With = "X-Requested-With";
    private const string DS = "DS";
    private const string Referer = "Referer";
    private const string Application_Json = "application/json";
    private const string com_mihoyo_hyperion = "com.mihoyo.hyperion";
    private const string x_rpc_app_version = "x-rpc-app_version";
    private const string x_rpc_device_id = "x-rpc-device_id";
    private const string x_rpc_client_type = "x-rpc-client_type";

    private const string AppVersion = "2.47.1";
    private static string UAContent => $"Mozilla/5.0 miHoYoBBS/{AppVersion}";
    private static readonly string DeviceId = Guid.NewGuid().ToString("D");

    private const string UrlAccountServerCn = "https://bbs-api.mihoyo.com/user/wapi/getUserFullInfo?gids=2";
    private const string UrlAccountServerGlobal = "https://bbs-api-os.hoyolab.com/community/painter/wapi/user/full";

    private const string UrlCharactersServerCn = "https://api-takumi.mihoyo.com/binding/api/getUserGameRolesByCookie?game_biz=hk4e_cn";
    private const string UrlCharactersServerGlobal = "https://api-os-takumi.mihoyo.com/binding/api/getUserGameRolesByCookie?game_biz=hk4e_global";

    private const string UrlBaseAvatarSideIconServerCn = "https://upload-bbs.mihoyo.com/game_record/genshin/character_side_icon/UI_AvatarIcon_Side_";
    private const string UrlBaseAvatarSideIconServerGlobal = "https://upload-os-bbs.mihoyo.com/game_record/genshin/character_side_icon/UI_AvatarIcon_Side_";

    private const string HeaderValueRefererServerCn = "https://webstatic.mihoyo.com/";
    private const string HeaderValueRefererServerGlobal = "https://webstatic-sea.hoyolab.com";

    private const string HeaderValueClientTypeServerCn = "5";
    private const string HeaderValueClientTypeServerGlobal = "2";
}

using Fischless.Hoyolab.Account;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Fischless.Hoyolab;

public partial class HoyolabClient
{
    private readonly HttpClient _httpClient;

    public HoyolabClient(HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? new(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.All });
    }

    private async Task<T> CommonSendAsync<T>(HttpRequestMessage request, CancellationToken? cancellationToken = null) where T : class
    {
        request.Headers.Add(Accept, Application_Json);
        request.Headers.Add(UserAgent, UAContent);
        var response = await _httpClient.SendAsync(request, cancellationToken ?? CancellationToken.None);
        response.EnsureSuccessStatusCode();
#if DEBUG
        var content = await response.Content.ReadAsStringAsync();
        var responseData = JsonSerializer.Deserialize<HoyolabBaseWrapper<T>>(content);
#else
        var responseData = await response.Content.ReadFromJsonAsync<HoyolabBaseWrapper<T>>();
#endif
        if (responseData is null)
        {
            throw new HoyolabException(-1, "Can not parse the response body.");
        }
        if (responseData.ReturnCode != 0)
        {
            throw new HoyolabException(responseData.ReturnCode, responseData.Message);
        }
        return responseData.Data!;
    }

    public async Task<HoyolabUserInfo> GetHoyolabUserInfoAsync(HoyolabRegion region, string cookie, CancellationToken? cancellationToken = null)
    {
        if (string.IsNullOrWhiteSpace(cookie))
        {
            throw new ArgumentNullException(nameof(cookie));
        }
        var url = region switch
        {
            HoyolabRegion.CN => UrlAccountServerCn,
            HoyolabRegion.OVERSEA or _ => UrlAccountServerGlobal,
        };
        var clientType = region switch
        {
            HoyolabRegion.CN => HeaderValueClientTypeServerCn,
            HoyolabRegion.OVERSEA or _ => HeaderValueClientTypeServerGlobal,
        };
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add(Cookie, cookie);
        request.Headers.Add(Referer, "https://bbs.mihoyo.com/");
        request.Headers.Add(DS, DynamicSecret.CreateSecret());
        request.Headers.Add(x_rpc_app_version, AppVersion);
        request.Headers.Add(x_rpc_device_id, DeviceId);
        request.Headers.Add(x_rpc_client_type, clientType);
        var data = await CommonSendAsync<HoyolabUserInfoWrapper>(request, cancellationToken);
        data.HoyolabUserInfo!.Cookie = cookie;
        return data.HoyolabUserInfo;
    }

    public async Task<List<GenshinRoleInfo>> GetGenshinRoleInfosAsync(HoyolabRegion region, string cookie, CancellationToken? cancellationToken = null)
    {
        if (string.IsNullOrWhiteSpace(cookie))
        {
            throw new ArgumentNullException(nameof(cookie));
        }
        var url = region switch
        {
            HoyolabRegion.CN => UrlCharactersServerCn,
            HoyolabRegion.OVERSEA or _ => UrlCharactersServerGlobal,
        };
        var referer = region switch
        {
            HoyolabRegion.CN => HeaderValueRefererServerCn,
            HoyolabRegion.OVERSEA or _ => HeaderValueRefererServerGlobal,
        };
        var clientType = region switch
        {
            HoyolabRegion.CN => HeaderValueClientTypeServerCn,
            HoyolabRegion.OVERSEA or _ => HeaderValueClientTypeServerGlobal,
        };
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add(Cookie, cookie);
        request.Headers.Add(DS, DynamicSecret.CreateSecret2(url));
        request.Headers.Add(X_Reuqest_With, com_mihoyo_hyperion);
        request.Headers.Add(x_rpc_app_version, AppVersion);
        request.Headers.Add(x_rpc_client_type, clientType);
        request.Headers.Add(Referer, referer);
        var data = await CommonSendAsync<GenshinRoleInfoWrapper>(request, cancellationToken);
        data.List?.ForEach(x => x.Cookie = cookie);
        return data.List ?? new List<GenshinRoleInfo>();
    }
}

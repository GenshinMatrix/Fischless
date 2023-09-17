using Fischless.Hoyolab;

namespace Fischless.Fetch.Launch;

public static class HoyolabRegionExtension
{
    public static HoyolabRegion ToHoyolabRegion(this string? server) => server switch
    {
        GILauncher.RegionOVERSEA => HoyolabRegion.OVERSEA,
        GILauncher.RegionCN or _ => HoyolabRegion.CN,
    };
}

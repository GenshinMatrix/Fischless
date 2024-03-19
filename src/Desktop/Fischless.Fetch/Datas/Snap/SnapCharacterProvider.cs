using Fischless.Fetch.Datas.Core;
using Fischless.Fetch.ReShade;

namespace Fischless.Fetch.Datas.Snap;

public static class SnapCharacterProvider
{
    public static IEnumerable<SnapCharacterInfo> GetSnapCharacters()
    {
        yield return new SnapCharacterInfo()
        {
            Id = 10000002,
            Name = "神里绫华",
            Title = "白鹭霜华",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Ayaka_Card.png",
            FaceIcon = "UI_AvatarIcon_Ayaka.png",
            SideIcon = "UI_AvatarIcon_Side_Ayaka.png",
            GachaCard = "UI_Gacha_AvatarIcon_Ayaka.png",
            GachaSplash = "UI_Gacha_AvatarImg_Ayaka.png",
            SortId = 34,
            BeginTime = "2021-07-20T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 200201,
                    CharacterId = 10000002,
                    Name = "花时来信",
                    IsDefault = false,
                    Card = "UI_AvatarIcon_AyakaCostumeFruhling_Card.png",
                    FaceIcon = "UI_AvatarIcon_AyakaCostumeFruhling.png",
                    SideIcon = "UI_AvatarIcon_Side_AyakaCostumeFruhling.png",
                    GachaSplash = "UI_Costume_AyakaCostumeFruhling.png",
                    TextureOverride = null,
                },
                new SnapCharacterOutfit()
                {
                    Id = 200200,
                    CharacterId = 10000002,
                    Name = "莹辉流华",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000002),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000003,
            Name = "琴",
            Title = "蒲公英骑士",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Wind,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Qin_Card.png",
            FaceIcon = "UI_AvatarIcon_Qin.png",
            SideIcon = "UI_AvatarIcon_Side_Qin.png",
            GachaCard = "UI_Gacha_AvatarIcon_Qin.png",
            GachaSplash = "UI_Gacha_AvatarImg_Qin.png",
            SortId = 9,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 200301,
                    CharacterId = 10000003,
                    Name = "海风之梦",
                    IsDefault = false,
                    Card = "UI_AvatarIcon_QinCostumeSea_Card.png",
                    FaceIcon = "UI_AvatarIcon_QinCostumeSea.png",
                    SideIcon = "UI_AvatarIcon_Side_QinCostumeSea.png",
                    GachaSplash = "UI_Costume_QinCostumeSea.png",
                },
                new SnapCharacterOutfit()
                {
                    Id = 200302,
                    CharacterId = 10000003,
                    Name = "古恩希尔德的传承",
                    IsDefault = false,
                    Card = "UI_AvatarIcon_QinCostumeWic_Card.png",
                    FaceIcon = "UI_AvatarIcon_QinCostumeWic.png",
                    SideIcon = "UI_AvatarIcon_Side_QinCostumeWic.png",
                    GachaSplash = "UI_Costume_QinCostumeWic.png",
                    TextureOverride = null,
                },
                new SnapCharacterOutfit()
                {
                    Id = 200300,
                    CharacterId = 10000003,
                    Name = "风之虔护",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000003),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000005,
            Name = "空",
            Title = "旅行者",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.None,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_PlayerBoy_Card.png",
            FaceIcon = "UI_AvatarIcon_PlayerBoy.png",
            SideIcon = "UI_AvatarIcon_Side_PlayerBoy.png",
            GachaCard = "UI_Gacha_AvatarIcon_PlayerBoy.png",
            GachaSplash = "UI_Gacha_AvatarImg_PlayerBoy.png",
            SortId = 0,
            BeginTime = "0001-01-01T00:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 200500,
                    CharacterId = 10000005,
                    Name = "初升之星",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000005),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000006,
            Name = "丽莎",
            Title = "蔷薇魔女",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Electro,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Lisa_Card.png",
            FaceIcon = "UI_AvatarIcon_Lisa.png",
            SideIcon = "UI_AvatarIcon_Side_Lisa.png",
            GachaCard = "UI_Gacha_AvatarIcon_Lisa.png",
            GachaSplash = "UI_Gacha_AvatarImg_Lisa.png",
            SortId = 21,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 200601,
                    CharacterId = 10000006,
                    Name = "叶隐芳名",
                    IsDefault = false,
                    Card = "UI_AvatarIcon_LisaCostumeStudentin_Card.png",
                    FaceIcon = "UI_AvatarIcon_LisaCostumeStudentin.png",
                    SideIcon = "UI_AvatarIcon_Side_LisaCostumeStudentin.png",
                    GachaSplash = "UI_Costume_LisaCostumeStudentin.png",
                    TextureOverride = null,
                },
                new SnapCharacterOutfit()
                {
                    Id = 200600,
                    CharacterId = 10000006,
                    Name = "绀紫蔷薇",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000006),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000007,
            Name = "荧",
            Title = "旅行者",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.None,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_PlayerGirl_Card.png",
            FaceIcon = "UI_AvatarIcon_PlayerGirl.png",
            SideIcon = "UI_AvatarIcon_Side_PlayerGirl.png",
            GachaCard = "UI_Gacha_AvatarIcon_PlayerGirl.png",
            GachaSplash = "UI_Gacha_AvatarImg_PlayerGirl.png",
            SortId = 0,
            BeginTime = "0001-01-01T00:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 200700,
                    CharacterId = 10000007,
                    Name = "初升之星",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000007),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000014,
            Name = "芭芭拉",
            Title = "闪耀偶像",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Water,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Barbara_Card.png",
            FaceIcon = "UI_AvatarIcon_Barbara.png",
            SideIcon = "UI_AvatarIcon_Side_Barbara.png",
            GachaCard = "UI_Gacha_AvatarIcon_Barbara.png",
            GachaSplash = "UI_Gacha_AvatarImg_Barbara.png",
            SortId = 20,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 201401,
                    CharacterId = 10000014,
                    Name = "闪耀协奏",
                    IsDefault = false,
                    Card = "UI_AvatarIcon_BarbaraCostumeSummertime_Card.png",
                    FaceIcon = "UI_AvatarIcon_BarbaraCostumeSummertime.png",
                    SideIcon = "UI_AvatarIcon_Side_BarbaraCostumeSummertime.png",
                    GachaSplash = "UI_Costume_BarbaraCostumeSummertime.png",
                    TextureOverride = null,
                },
                new SnapCharacterOutfit()
                {
                    Id = 201400,
                    CharacterId = 10000014,
                    Name = "纯真期待",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000014),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000015,
            Name = "凯亚",
            Title = "寒风剑士",
            Rarity = 4,
            Gender = 0,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Kaeya_Card.png",
            FaceIcon = "UI_AvatarIcon_Kaeya.png",
            SideIcon = "UI_AvatarIcon_Side_Kaeya.png",
            GachaCard = "UI_Gacha_AvatarIcon_Kaeya.png",
            GachaSplash = "UI_Gacha_AvatarImg_Kaeya.png",
            SortId = 22,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 201501,
                    CharacterId = 10000015,
                    Name = "帆影游风",
                    IsDefault = false,
                    Card = "UI_AvatarIcon_KaeyaCostumeDancer_Card.png",
                    FaceIcon = "UI_AvatarIcon_KaeyaCostumeDancer.png",
                    SideIcon = "UI_AvatarIcon_Side_KaeyaCostumeDancer.png",
                    GachaSplash = "UI_Costume_KaeyaCostumeDancer.png",
                    TextureOverride = null,
                },
                new SnapCharacterOutfit()
                {
                    Id = 201500,
                    CharacterId = 10000015,
                    Name = "冰上飞羽",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000015),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000016,
            Name = "迪卢克",
            Title = "晨曦的暗面",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Fire,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Diluc_Card.png",
            FaceIcon = "UI_AvatarIcon_Diluc.png",
            SideIcon = "UI_AvatarIcon_Side_Diluc.png",
            GachaCard = "UI_Gacha_AvatarIcon_Diluc.png",
            GachaSplash = "UI_Gacha_AvatarImg_Diluc.png",
            SortId = 8,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 201601,
                    CharacterId = 10000016,
                    Name = "殷红终夜",
                    IsDefault = false,
                    Card = "UI_AvatarIcon_DilucCostumeFlamme_Card.png",
                    FaceIcon = "UI_AvatarIcon_DilucCostumeFlamme.png",
                    SideIcon = "UI_AvatarIcon_Side_DilucCostumeFlamme.png",
                    GachaSplash = "UI_Costume_DilucCostumeFlamme.png",
                    TextureOverride = null,
                },
                new SnapCharacterOutfit()
                {
                    Id = 201600,
                    CharacterId = 10000016,
                    Name = "夜行暗火",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000016),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000020,
            Name = "雷泽",
            Title = "狼少年",
            Rarity = 4,
            Gender = 0,
            Element = ElementType.Electro,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Razor_Card.png",
            FaceIcon = "UI_AvatarIcon_Razor.png",
            SideIcon = "UI_AvatarIcon_Side_Razor.png",
            GachaCard = "UI_Gacha_AvatarIcon_Razor.png",
            GachaSplash = "UI_Gacha_AvatarImg_Razor.png",
            SortId = 19,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 202000,
                    CharacterId = 10000020,
                    Name = "奔行原野",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000020),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000021,
            Name = "安柏",
            Title = "飞行冠军",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Fire,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Ambor_Card.png",
            FaceIcon = "UI_AvatarIcon_Ambor.png",
            SideIcon = "UI_AvatarIcon_Side_Ambor.png",
            GachaCard = "UI_Gacha_AvatarIcon_Ambor.png",
            GachaSplash = "UI_Gacha_AvatarImg_Ambor.png",
            SortId = 23,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 202101,
                    CharacterId = 10000021,
                    Name = "100%侦察骑士",
                    IsDefault = false,
                    Card = "UI_AvatarIcon_AmborCostumeWic_Card.png",
                    FaceIcon = "UI_AvatarIcon_AmborCostumeWic.png",
                    SideIcon = "UI_AvatarIcon_Side_AmborCostumeWic.png",
                    GachaSplash = "UI_Costume_AmborCostumeWic.png",
                    TextureOverride = null,
                },
                new SnapCharacterOutfit()
                {
                    Id = 202100,
                    CharacterId = 10000021,
                    Name = "「满分的侦察骑士」",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000021),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000022,
            Name = "温迪",
            Title = "风色诗人",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Wind,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Venti_Card.png",
            FaceIcon = "UI_AvatarIcon_Venti.png",
            SideIcon = "UI_AvatarIcon_Side_Venti.png",
            GachaCard = "UI_Gacha_AvatarIcon_Venti.png",
            GachaSplash = "UI_Gacha_AvatarImg_Venti.png",
            SortId = 4,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 202200,
                    CharacterId = 10000022,
                    Name = "青风结诗",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000022),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000023,
            Name = "香菱",
            Title = "万民百味",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Fire,
            WeaponType = WeaponType.Polearm,
            Card = "UI_AvatarIcon_Xiangling_Card.png",
            FaceIcon = "UI_AvatarIcon_Xiangling.png",
            SideIcon = "UI_AvatarIcon_Side_Xiangling.png",
            GachaCard = "UI_Gacha_AvatarIcon_Xiangling.png",
            GachaSplash = "UI_Gacha_AvatarImg_Xiangling.png",
            SortId = 18,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 202300,
                    CharacterId = 10000023,
                    Name = "椒红姜黄",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000023),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000024,
            Name = "北斗",
            Title = "无冕的龙王",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Electro,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Beidou_Card.png",
            FaceIcon = "UI_AvatarIcon_Beidou.png",
            SideIcon = "UI_AvatarIcon_Side_Beidou.png",
            GachaCard = "UI_Gacha_AvatarIcon_Beidou.png",
            GachaSplash = "UI_Gacha_AvatarImg_Beidou.png",
            SortId = 17,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 202400,
                    CharacterId = 10000024,
                    Name = "赫浪连涛",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000024),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000025,
            Name = "行秋",
            Title = "少年春衫薄",
            Rarity = 4,
            Gender = 0,
            Element = ElementType.Water,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Xingqiu_Card.png",
            FaceIcon = "UI_AvatarIcon_Xingqiu.png",
            SideIcon = "UI_AvatarIcon_Side_Xingqiu.png",
            GachaCard = "UI_Gacha_AvatarIcon_Xingqiu.png",
            GachaSplash = "UI_Gacha_AvatarImg_Xingqiu.png",
            SortId = 16,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 202500,
                    CharacterId = 10000025,
                    Name = "锦钰兰衫",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000025),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000026,
            Name = "魈",
            Title = "护法夜叉",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Wind,
            WeaponType = WeaponType.Polearm,
            Card = "UI_AvatarIcon_Xiao_Card.png",
            FaceIcon = "UI_AvatarIcon_Xiao.png",
            SideIcon = "UI_AvatarIcon_Side_Xiao.png",
            GachaCard = "UI_Gacha_AvatarIcon_Xiao.png",
            GachaSplash = "UI_Gacha_AvatarImg_Xiao.png",
            SortId = 28,
            BeginTime = "2021-02-01T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 202600,
                    CharacterId = 10000026,
                    Name = "千秋竞岁",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000026),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000027,
            Name = "凝光",
            Title = "掩月天权",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Rock,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Ningguang_Card.png",
            FaceIcon = "UI_AvatarIcon_Ningguang.png",
            SideIcon = "UI_AvatarIcon_Side_Ningguang.png",
            GachaCard = "UI_Gacha_AvatarIcon_Ningguang.png",
            GachaSplash = "UI_Gacha_AvatarImg_Ningguang.png",
            SortId = 15,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 202701,
                    CharacterId = 10000027,
                    Name = "纱中幽兰",
                    IsDefault = false,
                    Card = "UI_AvatarIcon_NingguangCostumeFloral_Card.png",
                    FaceIcon = "UI_AvatarIcon_NingguangCostumeFloral.png",
                    SideIcon = "UI_AvatarIcon_Side_NingguangCostumeFloral.png",
                    GachaSplash = "UI_Costume_NingguangCostumeFloral.png",
                    TextureOverride = null,
                },
                new SnapCharacterOutfit()
                {
                    Id = 202700,
                    CharacterId = 10000027,
                    Name = "玑玉琼金",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000027),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000029,
            Name = "可莉",
            Title = "逃跑的太阳",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Fire,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Klee_Card.png",
            FaceIcon = "UI_AvatarIcon_Klee.png",
            SideIcon = "UI_AvatarIcon_Side_Klee.png",
            GachaCard = "UI_Gacha_AvatarIcon_Klee.png",
            GachaSplash = "UI_Gacha_AvatarImg_Klee.png",
            SortId = 3,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 202901,
                    CharacterId = 10000029,
                    Name = "琪花星烛",
                    IsDefault = false,
                    Card = "UI_AvatarIcon_KleeCostumeWitch_Card.png",
                    FaceIcon = "UI_AvatarIcon_KleeCostumeWitch.png",
                    SideIcon = "UI_AvatarIcon_Side_KleeCostumeWitch.png",
                    GachaSplash = "UI_Costume_KleeCostumeWitch.png",
                    TextureOverride = null,
                },
                new SnapCharacterOutfit()
                {
                    Id = 202900,
                    CharacterId = 10000029,
                    Name = "轻灵火花",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000029),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000030,
            Name = "钟离",
            Title = "尘世闲游",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Rock,
            WeaponType = WeaponType.Polearm,
            Card = "UI_AvatarIcon_Zhongli_Card.png",
            FaceIcon = "UI_AvatarIcon_Zhongli.png",
            SideIcon = "UI_AvatarIcon_Side_Zhongli.png",
            GachaCard = "UI_Gacha_AvatarIcon_Zhongli.png",
            GachaSplash = "UI_Gacha_AvatarImg_Zhongli.png",
            SortId = 24,
            BeginTime = "2020-12-01T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 203000,
                    CharacterId = 10000030,
                    Name = "凡世致身",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000030),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000031,
            Name = "菲谢尔",
            Title = "断罪皇女！！",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Electro,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Fischl_Card.png",
            FaceIcon = "UI_AvatarIcon_Fischl.png",
            SideIcon = "UI_AvatarIcon_Side_Fischl.png",
            GachaCard = "UI_Gacha_AvatarIcon_Fischl.png",
            GachaSplash = "UI_Gacha_AvatarImg_Fischl.png",
            SortId = 14,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 203101,
                    CharacterId = 10000031,
                    Name = "极夜真梦",
                    IsDefault = false,
                    Card = "UI_AvatarIcon_FischlCostumeHighness_Card.png",
                    FaceIcon = "UI_AvatarIcon_FischlCostumeHighness.png",
                    SideIcon = "UI_AvatarIcon_Side_FischlCostumeHighness.png",
                    GachaSplash = "UI_Costume_FischlCostumeHighness.png",
                    TextureOverride = null,
                },
                new SnapCharacterOutfit()
                {
                    Id = 203100,
                    CharacterId = 10000031,
                    Name = "绮夜圣礼",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000031),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000032,
            Name = "班尼特",
            Title = "命运试金石",
            Rarity = 4,
            Gender = 0,
            Element = ElementType.Fire,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Bennett_Card.png",
            FaceIcon = "UI_AvatarIcon_Bennett.png",
            SideIcon = "UI_AvatarIcon_Side_Bennett.png",
            GachaCard = "UI_Gacha_AvatarIcon_Bennett.png",
            GachaSplash = "UI_Gacha_AvatarImg_Bennett.png",
            SortId = 13,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 203200,
                    CharacterId = 10000032,
                    Name = "噩运绕行",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000032),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000033,
            Name = "达达利亚",
            Title = "「公子」",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Water,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Tartaglia_Card.png",
            FaceIcon = "UI_AvatarIcon_Tartaglia.png",
            SideIcon = "UI_AvatarIcon_Side_Tartaglia.png",
            GachaCard = "UI_Gacha_AvatarIcon_Tartaglia.png",
            GachaSplash = "UI_Gacha_AvatarImg_Tartaglia.png",
            SortId = 1,
            BeginTime = "2020-11-10T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 203300,
                    CharacterId = 10000033,
                    Name = "荣光之刃",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000033),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000034,
            Name = "诺艾尔",
            Title = "未授勋之花",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Rock,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Noel_Card.png",
            FaceIcon = "UI_AvatarIcon_Noel.png",
            SideIcon = "UI_AvatarIcon_Side_Noel.png",
            GachaCard = "UI_Gacha_AvatarIcon_Noel.png",
            GachaSplash = "UI_Gacha_AvatarImg_Noel.png",
            SortId = 12,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 203400,
                    CharacterId = 10000034,
                    Name = "甲铁玫瑰",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000034),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000035,
            Name = "七七",
            Title = "冻冻回魂夜",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Qiqi_Card.png",
            FaceIcon = "UI_AvatarIcon_Qiqi.png",
            SideIcon = "UI_AvatarIcon_Side_Qiqi.png",
            GachaCard = "UI_Gacha_AvatarIcon_Qiqi.png",
            GachaSplash = "UI_Gacha_AvatarImg_Qiqi.png",
            SortId = 7,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 203500,
                    CharacterId = 10000035,
                    Name = "夏冰梦身",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000035),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000036,
            Name = "重云",
            Title = "雪融有踪",
            Rarity = 4,
            Gender = 0,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Chongyun_Card.png",
            FaceIcon = "UI_AvatarIcon_Chongyun.png",
            SideIcon = "UI_AvatarIcon_Side_Chongyun.png",
            GachaCard = "UI_Gacha_AvatarIcon_Chongyun.png",
            GachaSplash = "UI_Gacha_AvatarImg_Chongyun.png",
            SortId = 11,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 203600,
                    CharacterId = 10000036,
                    Name = "清正灵台",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000036),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000037,
            Name = "甘雨",
            Title = "循循守月",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Ganyu_Card.png",
            FaceIcon = "UI_AvatarIcon_Ganyu.png",
            SideIcon = "UI_AvatarIcon_Side_Ganyu.png",
            GachaCard = "UI_Gacha_AvatarIcon_Ganyu.png",
            GachaSplash = "UI_Gacha_AvatarImg_Ganyu.png",
            SortId = 26,
            BeginTime = "2021-01-12T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 203700,
                    CharacterId = 10000037,
                    Name = "霜麟聚露",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000037),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000038,
            Name = "阿贝多",
            Title = "白垩之子",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Rock,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Albedo_Card.png",
            FaceIcon = "UI_AvatarIcon_Albedo.png",
            SideIcon = "UI_AvatarIcon_Side_Albedo.png",
            GachaCard = "UI_Gacha_AvatarIcon_Albedo.png",
            GachaSplash = "UI_Gacha_AvatarImg_Albedo.png",
            SortId = 27,
            BeginTime = "2020-12-21T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 203800,
                    CharacterId = 10000038,
                    Name = "朔分星芒",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000038),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000039,
            Name = "迪奥娜",
            Title = "猫尾特调",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Diona_Card.png",
            FaceIcon = "UI_AvatarIcon_Diona.png",
            SideIcon = "UI_AvatarIcon_Side_Diona.png",
            GachaCard = "UI_Gacha_AvatarIcon_Diona.png",
            GachaSplash = "UI_Gacha_AvatarImg_Diona.png",
            SortId = 2,
            BeginTime = "2020-11-10T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 203900,
                    CharacterId = 10000039,
                    Name = "粉糖青酿",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000039),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000041,
            Name = "莫娜",
            Title = "星天水镜",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Water,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Mona_Card.png",
            FaceIcon = "UI_AvatarIcon_Mona.png",
            SideIcon = "UI_AvatarIcon_Side_Mona.png",
            GachaCard = "UI_Gacha_AvatarIcon_Mona.png",
            GachaSplash = "UI_Gacha_AvatarImg_Mona.png",
            SortId = 6,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 204101,
                    CharacterId = 10000041,
                    Name = "星与月之约",
                    IsDefault = false,
                    Card = "UI_AvatarIcon_MonaCostumeWic_Card.png",
                    FaceIcon = "UI_AvatarIcon_MonaCostumeWic.png",
                    SideIcon = "UI_AvatarIcon_Side_MonaCostumeWic.png",
                    GachaSplash = "UI_Costume_MonaCostumeWic.png",
                    TextureOverride = null,
                },
                new SnapCharacterOutfit()
                {
                    Id = 204100,
                    CharacterId = 10000041,
                    Name = "星命流转",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000041),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000042,
            Name = "刻晴",
            Title = "霆霓快雨",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Electro,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Keqing_Card.png",
            FaceIcon = "UI_AvatarIcon_Keqing.png",
            SideIcon = "UI_AvatarIcon_Side_Keqing.png",
            GachaCard = "UI_Gacha_AvatarIcon_Keqing.png",
            GachaSplash = "UI_Gacha_AvatarImg_Keqing.png",
            SortId = 5,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 204201,
                    CharacterId = 10000042,
                    Name = "霓裾翩跹",
                    IsDefault = false,
                    Card = "UI_AvatarIcon_KeqingCostumeFeather_Card.png",
                    FaceIcon = "UI_AvatarIcon_KeqingCostumeFeather.png",
                    SideIcon = "UI_AvatarIcon_Side_KeqingCostumeFeather.png",
                    GachaSplash = "UI_Costume_KeqingCostumeFeather.png",
                    TextureOverride = null,
                },
                new SnapCharacterOutfit()
                {
                    Id = 204200,
                    CharacterId = 10000042,
                    Name = "雷雳寰宇",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000042),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000043,
            Name = "砂糖",
            Title = "无害甜度",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Wind,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Sucrose_Card.png",
            FaceIcon = "UI_AvatarIcon_Sucrose.png",
            SideIcon = "UI_AvatarIcon_Side_Sucrose.png",
            GachaCard = "UI_Gacha_AvatarIcon_Sucrose.png",
            GachaSplash = "UI_Gacha_AvatarImg_Sucrose.png",
            SortId = 10,
            BeginTime = "2020-09-14T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 204300,
                    CharacterId = 10000043,
                    Name = "萌生的灵风",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000043),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000044,
            Name = "辛焱",
            Title = "燥热旋律",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Fire,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Xinyan_Card.png",
            FaceIcon = "UI_AvatarIcon_Xinyan.png",
            SideIcon = "UI_AvatarIcon_Side_Xinyan.png",
            GachaCard = "UI_Gacha_AvatarIcon_Xinyan.png",
            GachaSplash = "UI_Gacha_AvatarImg_Xinyan.png",
            SortId = 25,
            BeginTime = "2020-12-01T20:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 204400,
                    CharacterId = 10000044,
                    Name = "灵魂热望",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000044),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000045,
            Name = "罗莎莉亚",
            Title = "棘冠恩典",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Polearm,
            Card = "UI_AvatarIcon_Rosaria_Card.png",
            FaceIcon = "UI_AvatarIcon_Rosaria.png",
            SideIcon = "UI_AvatarIcon_Side_Rosaria.png",
            GachaCard = "UI_Gacha_AvatarIcon_Rosaria.png",
            GachaSplash = "UI_Gacha_AvatarImg_Rosaria.png",
            SortId = 30,
            BeginTime = "2021-04-06T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 204501,
                    CharacterId = 10000045,
                    Name = "致教会自由人",
                    IsDefault = false,
                    Card = "UI_AvatarIcon_RosariaCostumeWic_Card.png",
                    FaceIcon = "UI_AvatarIcon_RosariaCostumeWic.png",
                    SideIcon = "UI_AvatarIcon_Side_RosariaCostumeWic.png",
                    GachaSplash = "UI_Costume_RosariaCostumeWic.png",
                    TextureOverride = null,
                },
                new SnapCharacterOutfit()
                {
                    Id = 204500,
                    CharacterId = 10000045,
                    Name = "处刑棘刺",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000045),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000046,
            Name = "胡桃",
            Title = "雪霁梅香",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Fire,
            WeaponType = WeaponType.Polearm,
            Card = "UI_AvatarIcon_Hutao_Card.png",
            FaceIcon = "UI_AvatarIcon_Hutao.png",
            SideIcon = "UI_AvatarIcon_Side_Hutao.png",
            GachaCard = "UI_Gacha_AvatarIcon_Hutao.png",
            GachaSplash = "UI_Gacha_AvatarImg_Hutao.png",
            SortId = 29,
            BeginTime = "2021-03-02T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 204600,
                    CharacterId = 10000046,
                    Name = "折梅留芳",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000046),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000047,
            Name = "枫原万叶",
            Title = "红叶逐荒波",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Wind,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Kazuha_Card.png",
            FaceIcon = "UI_AvatarIcon_Kazuha.png",
            SideIcon = "UI_AvatarIcon_Side_Kazuha.png",
            GachaCard = "UI_Gacha_AvatarIcon_Kazuha.png",
            GachaSplash = "UI_Gacha_AvatarImg_Kazuha.png",
            SortId = 33,
            BeginTime = "2021-06-29T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 204700,
                    CharacterId = 10000047,
                    Name = "叶落天涯",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000047),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000048,
            Name = "烟绯",
            Title = "智明无邪",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Fire,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Feiyan_Card.png",
            FaceIcon = "UI_AvatarIcon_Feiyan.png",
            SideIcon = "UI_AvatarIcon_Side_Feiyan.png",
            GachaCard = "UI_Gacha_AvatarIcon_Feiyan.png",
            GachaSplash = "UI_Gacha_AvatarImg_Feiyan.png",
            SortId = 31,
            BeginTime = "2021-04-26T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 204800,
                    CharacterId = 10000048,
                    Name = "金科赤律",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000048),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000049,
            Name = "宵宫",
            Title = "琉焰华舞",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Fire,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Yoimiya_Card.png",
            FaceIcon = "UI_AvatarIcon_Yoimiya.png",
            SideIcon = "UI_AvatarIcon_Side_Yoimiya.png",
            GachaCard = "UI_Gacha_AvatarIcon_Yoimiya.png",
            GachaSplash = "UI_Gacha_AvatarImg_Yoimiya.png",
            SortId = 36,
            BeginTime = "2021-08-10T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 204900,
                    CharacterId = 10000049,
                    Name = "金鱼花火",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000049),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000050,
            Name = "托马",
            Title = "渡来介者",
            Rarity = 4,
            Gender = 0,
            Element = ElementType.Fire,
            WeaponType = WeaponType.Polearm,
            Card = "UI_AvatarIcon_Tohma_Card.png",
            FaceIcon = "UI_AvatarIcon_Tohma.png",
            SideIcon = "UI_AvatarIcon_Side_Tohma.png",
            GachaCard = "UI_Gacha_AvatarIcon_Tohma.png",
            GachaSplash = "UI_Gacha_AvatarImg_Tohma.png",
            SortId = 41,
            BeginTime = "2021-11-02T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 205000,
                    CharacterId = 10000050,
                    Name = "焰之赤武",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000050),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000051,
            Name = "优菈",
            Title = "浪沫的旋舞",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Eula_Card.png",
            FaceIcon = "UI_AvatarIcon_Eula.png",
            SideIcon = "UI_AvatarIcon_Side_Eula.png",
            GachaCard = "UI_Gacha_AvatarIcon_Eula.png",
            GachaSplash = "UI_Gacha_AvatarImg_Eula.png",
            SortId = 32,
            BeginTime = "2021-05-18T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 205100,
                    CharacterId = 10000051,
                    Name = "浪尖之舞",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000051),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000052,
            Name = "雷电将军",
            Title = "一心净土",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Electro,
            WeaponType = WeaponType.Polearm,
            Card = "UI_AvatarIcon_Shougun_Card.png",
            FaceIcon = "UI_AvatarIcon_Shougun.png",
            SideIcon = "UI_AvatarIcon_Side_Shougun.png",
            GachaCard = "UI_Gacha_AvatarIcon_Shougun.png",
            GachaSplash = "UI_Gacha_AvatarImg_Shougun.png",
            SortId = 39,
            BeginTime = "2021-08-31T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 205200,
                    CharacterId = 10000052,
                    Name = "御建鸣神主尊之典",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000052),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000053,
            Name = "早柚",
            Title = "忍里之貉",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Wind,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Sayu_Card.png",
            FaceIcon = "UI_AvatarIcon_Sayu.png",
            SideIcon = "UI_AvatarIcon_Side_Sayu.png",
            GachaCard = "UI_Gacha_AvatarIcon_Sayu.png",
            GachaSplash = "UI_Gacha_AvatarImg_Sayu.png",
            SortId = 35,
            BeginTime = "2021-08-10T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 205300,
                    CharacterId = 10000053,
                    Name = "小貉服",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000053),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000054,
            Name = "珊瑚宫心海",
            Title = "真珠之智",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Water,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Kokomi_Card.png",
            FaceIcon = "UI_AvatarIcon_Kokomi.png",
            SideIcon = "UI_AvatarIcon_Side_Kokomi.png",
            GachaCard = "UI_Gacha_AvatarIcon_Kokomi.png",
            GachaSplash = "UI_Gacha_AvatarImg_Kokomi.png",
            SortId = 40,
            BeginTime = "2021-09-21T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 205400,
                    CharacterId = 10000054,
                    Name = "珊骨粼波",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000054),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000055,
            Name = "五郎",
            Title = "戎犬锵锵",
            Rarity = 4,
            Gender = 0,
            Element = ElementType.Rock,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Gorou_Card.png",
            FaceIcon = "UI_AvatarIcon_Gorou.png",
            SideIcon = "UI_AvatarIcon_Side_Gorou.png",
            GachaCard = "UI_Gacha_AvatarIcon_Gorou.png",
            GachaSplash = "UI_Gacha_AvatarImg_Gorou.png",
            SortId = 42,
            BeginTime = "2021-12-14T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 205500,
                    CharacterId = 10000055,
                    Name = "百狩之义铠",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000055),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000056,
            Name = "九条裟罗",
            Title = "黑羽鸣镝",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Electro,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Sara_Card.png",
            FaceIcon = "UI_AvatarIcon_Sara.png",
            SideIcon = "UI_AvatarIcon_Side_Sara.png",
            GachaCard = "UI_Gacha_AvatarIcon_Sara.png",
            GachaSplash = "UI_Gacha_AvatarImg_Sara.png",
            SortId = 38,
            BeginTime = "2021-08-31T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 205600,
                    CharacterId = 10000056,
                    Name = "严正净衣",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000056),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000057,
            Name = "荒泷一斗",
            Title = "花坂豪快",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Rock,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Itto_Card.png",
            FaceIcon = "UI_AvatarIcon_Itto.png",
            SideIcon = "UI_AvatarIcon_Side_Itto.png",
            GachaCard = "UI_Gacha_AvatarIcon_Itto.png",
            GachaSplash = "UI_Gacha_AvatarImg_Itto.png",
            SortId = 43,
            BeginTime = "2021-12-14T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 205700,
                    CharacterId = 10000057,
                    Name = "倾奇狂鬼",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000057),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000058,
            Name = "八重神子",
            Title = "浮世笑百姿",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Electro,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Yae_Card.png",
            FaceIcon = "UI_AvatarIcon_Yae.png",
            SideIcon = "UI_AvatarIcon_Side_Yae.png",
            GachaCard = "UI_Gacha_AvatarIcon_Yae.png",
            GachaSplash = "UI_Gacha_AvatarImg_Yae.png",
            SortId = 46,
            BeginTime = "2022-02-15T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 205800,
                    CharacterId = 10000058,
                    Name = "御子之谕",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000058),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000059,
            Name = "鹿野院平藏",
            Title = "心朝乂安",
            Rarity = 4,
            Gender = 0,
            Element = ElementType.Wind,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Heizo_Card.png",
            FaceIcon = "UI_AvatarIcon_Heizo.png",
            SideIcon = "UI_AvatarIcon_Side_Heizo.png",
            GachaCard = "UI_Gacha_AvatarIcon_Heizo.png",
            GachaSplash = "UI_Gacha_AvatarImg_Heizo.png",
            SortId = 50,
            BeginTime = "2022-07-12T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 205900,
                    CharacterId = 10000059,
                    Name = "弁武朝风",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000059),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000060,
            Name = "夜兰",
            Title = "兰生幽谷",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Water,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Yelan_Card.png",
            FaceIcon = "UI_AvatarIcon_Yelan.png",
            SideIcon = "UI_AvatarIcon_Side_Yelan.png",
            GachaCard = "UI_Gacha_AvatarIcon_Yelan.png",
            GachaSplash = "UI_Gacha_AvatarImg_Yelan.png",
            SortId = 48,
            BeginTime = "2022-05-30T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 206000,
                    CharacterId = 10000060,
                    Name = "于阑珊处",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000060),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000061,
            Name = "绮良良",
            Title = "檐宇猫游",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Grass,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Momoka_Card.png",
            FaceIcon = "UI_AvatarIcon_Momoka.png",
            SideIcon = "UI_AvatarIcon_Side_Momoka.png",
            GachaCard = "UI_Gacha_AvatarIcon_Momoka.png",
            GachaSplash = "UI_Gacha_AvatarImg_Momoka.png",
            SortId = 67,
            BeginTime = "2023-05-22T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 206100,
                    CharacterId = 10000061,
                    Name = "百花戏",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000061),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000062,
            Name = "埃洛伊",
            Title = "「异界的救世主」",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Aloy_Card.png",
            FaceIcon = "UI_AvatarIcon_Aloy.png",
            SideIcon = "UI_AvatarIcon_Side_Aloy.png",
            GachaCard = "UI_Gacha_AvatarIcon_Aloy.png",
            GachaSplash = "UI_Gacha_AvatarImg_Aloy.png",
            SortId = 37,
            BeginTime = "2021-08-31T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 206200,
                    CharacterId = 10000062,
                    Name = "机动猎手",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000062),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000063,
            Name = "申鹤",
            Title = "孤辰茕怀",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Polearm,
            Card = "UI_AvatarIcon_Shenhe_Card.png",
            FaceIcon = "UI_AvatarIcon_Shenhe.png",
            SideIcon = "UI_AvatarIcon_Side_Shenhe.png",
            GachaCard = "UI_Gacha_AvatarIcon_Shenhe.png",
            GachaSplash = "UI_Gacha_AvatarImg_Shenhe.png",
            SortId = 45,
            BeginTime = "2022-01-04T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 206300,
                    CharacterId = 10000063,
                    Name = "缚绝红尘",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000063),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000064,
            Name = "云堇",
            Title = "红毹婵娟",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Rock,
            WeaponType = WeaponType.Polearm,
            Card = "UI_AvatarIcon_Yunjin_Card.png",
            FaceIcon = "UI_AvatarIcon_Yunjin.png",
            SideIcon = "UI_AvatarIcon_Side_Yunjin.png",
            GachaCard = "UI_Gacha_AvatarIcon_Yunjin.png",
            GachaSplash = "UI_Gacha_AvatarImg_Yunjin.png",
            SortId = 44,
            BeginTime = "2022-01-04T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 206400,
                    CharacterId = 10000064,
                    Name = "氍毹烟云",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000064),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000065,
            Name = "久岐忍",
            Title = "烦恼刈除",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Electro,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Shinobu_Card.png",
            FaceIcon = "UI_AvatarIcon_Shinobu.png",
            SideIcon = "UI_AvatarIcon_Side_Shinobu.png",
            GachaCard = "UI_Gacha_AvatarIcon_Shinobu.png",
            GachaSplash = "UI_Gacha_AvatarImg_Shinobu.png",
            SortId = 49,
            BeginTime = "2022-06-21T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 206500,
                    CharacterId = 10000065,
                    Name = "荒泷鬼副手",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000065),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000066,
            Name = "神里绫人",
            Title = "磐祭叶守",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Water,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Ayato_Card.png",
            FaceIcon = "UI_AvatarIcon_Ayato.png",
            SideIcon = "UI_AvatarIcon_Side_Ayato.png",
            GachaCard = "UI_Gacha_AvatarIcon_Ayato.png",
            GachaSplash = "UI_Gacha_AvatarImg_Ayato.png",
            SortId = 47,
            BeginTime = "2022-03-29T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 206600,
                    CharacterId = 10000066,
                    Name = "绫罗锦绣",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000066),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000067,
            Name = "柯莱",
            Title = "萃念初蘖",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Grass,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Collei_Card.png",
            FaceIcon = "UI_AvatarIcon_Collei.png",
            SideIcon = "UI_AvatarIcon_Side_Collei.png",
            GachaCard = "UI_Gacha_AvatarIcon_Collei.png",
            GachaSplash = "UI_Gacha_AvatarImg_Collei.png",
            SortId = 51,
            BeginTime = "2022-08-23T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 206700,
                    CharacterId = 10000067,
                    Name = "一叶新生",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000067),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000068,
            Name = "多莉",
            Title = "梦园藏金",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Electro,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Dori_Card.png",
            FaceIcon = "UI_AvatarIcon_Dori.png",
            SideIcon = "UI_AvatarIcon_Side_Dori.png",
            GachaCard = "UI_Gacha_AvatarIcon_Dori.png",
            GachaSplash = "UI_Gacha_AvatarImg_Dori.png",
            SortId = 53,
            BeginTime = "2022-09-09T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 206800,
                    CharacterId = 10000068,
                    Name = "我爱摩拉",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000068),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000069,
            Name = "提纳里",
            Title = "浅蔚轻行",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Grass,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Tighnari_Card.png",
            FaceIcon = "UI_AvatarIcon_Tighnari.png",
            SideIcon = "UI_AvatarIcon_Side_Tighnari.png",
            GachaCard = "UI_Gacha_AvatarIcon_Tighnari.png",
            GachaSplash = "UI_Gacha_AvatarImg_Tighnari.png",
            SortId = 52,
            BeginTime = "2022-08-23T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 206900,
                    CharacterId = 10000069,
                    Name = "森林之歌",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000069),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000070,
            Name = "妮露",
            Title = "莲光落舞筵",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Water,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Nilou_Card.png",
            FaceIcon = "UI_AvatarIcon_Nilou.png",
            SideIcon = "UI_AvatarIcon_Side_Nilou.png",
            GachaCard = "UI_Gacha_AvatarIcon_Nilou.png",
            GachaSplash = "UI_Gacha_AvatarImg_Nilou.png",
            SortId = 56,
            BeginTime = "2022-10-14T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 207000,
                    CharacterId = 10000070,
                    Name = "非花非雾",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000070),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000071,
            Name = "赛诺",
            Title = "缄秘的裁遣",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Electro,
            WeaponType = WeaponType.Polearm,
            Card = "UI_AvatarIcon_Cyno_Card.png",
            FaceIcon = "UI_AvatarIcon_Cyno.png",
            SideIcon = "UI_AvatarIcon_Side_Cyno.png",
            GachaCard = "UI_Gacha_AvatarIcon_Cyno.png",
            GachaSplash = "UI_Gacha_AvatarImg_Cyno.png",
            SortId = 55,
            BeginTime = "2022-09-26T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 207100,
                    CharacterId = 10000071,
                    Name = "衡断之心",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000071),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000072,
            Name = "坎蒂丝",
            Title = "浮金的誓愿",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Water,
            WeaponType = WeaponType.Polearm,
            Card = "UI_AvatarIcon_Candace_Card.png",
            FaceIcon = "UI_AvatarIcon_Candace.png",
            SideIcon = "UI_AvatarIcon_Side_Candace.png",
            GachaCard = "UI_Gacha_AvatarIcon_Candace.png",
            GachaSplash = "UI_Gacha_AvatarImg_Candace.png",
            SortId = 54,
            BeginTime = "2022-09-26T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 207200,
                    CharacterId = 10000072,
                    Name = "沙漠与夜",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000072),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000073,
            Name = "纳西妲",
            Title = "白草净华",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Grass,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Nahida_Card.png",
            FaceIcon = "UI_AvatarIcon_Nahida.png",
            SideIcon = "UI_AvatarIcon_Side_Nahida.png",
            GachaCard = "UI_Gacha_AvatarIcon_Nahida.png",
            GachaSplash = "UI_Gacha_AvatarImg_Nahida.png",
            SortId = 57,
            BeginTime = "2022-10-31T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 207300,
                    CharacterId = 10000073,
                    Name = "万识一偈",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000073),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000074,
            Name = "莱依拉",
            Title = "绮思晚星",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Layla_Card.png",
            FaceIcon = "UI_AvatarIcon_Layla.png",
            SideIcon = "UI_AvatarIcon_Side_Layla.png",
            GachaCard = "UI_Gacha_AvatarIcon_Layla.png",
            GachaSplash = "UI_Gacha_AvatarImg_Layla.png",
            SortId = 58,
            BeginTime = "2022-11-18T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 207400,
                    CharacterId = 10000074,
                    Name = "梦里星",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000074),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000075,
            Name = "流浪者",
            Title = "久世浮倾",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Wind,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Wanderer_Card.png",
            FaceIcon = "UI_AvatarIcon_Wanderer.png",
            SideIcon = "UI_AvatarIcon_Side_Wanderer.png",
            GachaCard = "UI_Gacha_AvatarIcon_Wanderer.png",
            GachaSplash = "UI_Gacha_AvatarImg_Wanderer.png",
            SortId = 60,
            BeginTime = "2022-12-05T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 207500,
                    CharacterId = 10000075,
                    Name = "寂忿空相",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000075),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000076,
            Name = "珐露珊",
            Title = "机逐封秘",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Wind,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Faruzan_Card.png",
            FaceIcon = "UI_AvatarIcon_Faruzan.png",
            SideIcon = "UI_AvatarIcon_Side_Faruzan.png",
            GachaCard = "UI_Gacha_AvatarIcon_Faruzan.png",
            GachaSplash = "UI_Gacha_AvatarImg_Faruzan.png",
            SortId = 59,
            BeginTime = "2022-12-05T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 207600,
                    CharacterId = 10000076,
                    Name = "碧叶素华",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000076),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000077,
            Name = "瑶瑶",
            Title = "仙蕊玲珑",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Grass,
            WeaponType = WeaponType.Polearm,
            Card = "UI_AvatarIcon_Yaoyao_Card.png",
            FaceIcon = "UI_AvatarIcon_Yaoyao.png",
            SideIcon = "UI_AvatarIcon_Side_Yaoyao.png",
            GachaCard = "UI_Gacha_AvatarIcon_Yaoyao.png",
            GachaSplash = "UI_Gacha_AvatarImg_Yaoyao.png",
            SortId = 61,
            BeginTime = "2023-01-16T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 207700,
                    CharacterId = 10000077,
                    Name = "栗蕊木樨花",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000077),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000078,
            Name = "艾尔海森",
            Title = "诲韬诤言",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Grass,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Alhatham_Card.png",
            FaceIcon = "UI_AvatarIcon_Alhatham.png",
            SideIcon = "UI_AvatarIcon_Side_Alhatham.png",
            GachaCard = "UI_Gacha_AvatarIcon_Alhatham.png",
            GachaSplash = "UI_Gacha_AvatarImg_Alhatham.png",
            SortId = 62,
            BeginTime = "2023-01-16T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 207800,
                    CharacterId = 10000078,
                    Name = "崇理者",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000078),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000079,
            Name = "迪希雅",
            Title = "炽鬃之狮",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Fire,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Dehya_Card.png",
            FaceIcon = "UI_AvatarIcon_Dehya.png",
            SideIcon = "UI_AvatarIcon_Side_Dehya.png",
            GachaCard = "UI_Gacha_AvatarIcon_Dehya.png",
            GachaSplash = "UI_Gacha_AvatarImg_Dehya.png",
            SortId = 63,
            BeginTime = "2023-02-27T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 207900,
                    CharacterId = 10000079,
                    Name = "狮与赤阳",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000079),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000080,
            Name = "米卡",
            Title = "晴霜的标绘",
            Rarity = 4,
            Gender = 0,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Polearm,
            Card = "UI_AvatarIcon_Mika_Card.png",
            FaceIcon = "UI_AvatarIcon_Mika.png",
            SideIcon = "UI_AvatarIcon_Side_Mika.png",
            GachaCard = "UI_Gacha_AvatarIcon_Mika.png",
            GachaSplash = "UI_Gacha_AvatarImg_Mika.png",
            SortId = 64,
            BeginTime = "2023-03-21T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 208000,
                    CharacterId = 10000080,
                    Name = "高飞信标",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000080),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000081,
            Name = "卡维",
            Title = "天穹之镜",
            Rarity = 4,
            Gender = 0,
            Element = ElementType.Grass,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Kaveh_Card.png",
            FaceIcon = "UI_AvatarIcon_Kaveh.png",
            SideIcon = "UI_AvatarIcon_Side_Kaveh.png",
            GachaCard = "UI_Gacha_AvatarIcon_Kaveh.png",
            GachaSplash = "UI_Gacha_AvatarImg_Kaveh.png",
            SortId = 65,
            BeginTime = "2023-05-02T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 208100,
                    CharacterId = 10000081,
                    Name = "浴火的金羽",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000081),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000082,
            Name = "白术",
            Title = "遵生合和",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Grass,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Baizhuer_Card.png",
            FaceIcon = "UI_AvatarIcon_Baizhuer.png",
            SideIcon = "UI_AvatarIcon_Side_Baizhuer.png",
            GachaCard = "UI_Gacha_AvatarIcon_Baizhuer.png",
            GachaSplash = "UI_Gacha_AvatarImg_Baizhuer.png",
            SortId = 66,
            BeginTime = "2023-05-02T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 208200,
                    CharacterId = 10000082,
                    Name = "君臣佐使",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000082),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000083,
            Name = "琳妮特",
            Title = "丽影绮行",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Wind,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Linette_Card.png",
            FaceIcon = "UI_AvatarIcon_Linette.png",
            SideIcon = "UI_AvatarIcon_Side_Linette.png",
            GachaCard = "UI_Gacha_AvatarIcon_Linette.png",
            GachaSplash = "UI_Gacha_AvatarImg_Linette.png",
            SortId = 68,
            BeginTime = "2023-08-14T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 208300,
                    CharacterId = 10000083,
                    Name = "魅影猫",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000083),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000084,
            Name = "林尼",
            Title = "惑光幻戏",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Fire,
            WeaponType = WeaponType.Bow,
            Card = "UI_AvatarIcon_Liney_Card.png",
            FaceIcon = "UI_AvatarIcon_Liney.png",
            SideIcon = "UI_AvatarIcon_Side_Liney.png",
            GachaCard = "UI_Gacha_AvatarIcon_Liney.png",
            GachaSplash = "UI_Gacha_AvatarImg_Liney.png",
            SortId = 69,
            BeginTime = "2023-08-14T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 208400,
                    CharacterId = 10000084,
                    Name = "幻戏猫",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000084),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000085,
            Name = "菲米尼",
            Title = "潜怀遐梦",
            Rarity = 4,
            Gender = 0,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Freminet_Card.png",
            FaceIcon = "UI_AvatarIcon_Freminet.png",
            SideIcon = "UI_AvatarIcon_Side_Freminet.png",
            GachaCard = "UI_Gacha_AvatarIcon_Freminet.png",
            GachaSplash = "UI_Gacha_AvatarImg_Freminet.png",
            SortId = 70,
            BeginTime = "2023-09-05T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 208500,
                    CharacterId = 10000085,
                    Name = "冰的肌肤",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000085),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000086,
            Name = "莱欧斯利",
            Title = "寂罪的密使",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Wriothesley_Card.png",
            FaceIcon = "UI_AvatarIcon_Wriothesley.png",
            SideIcon = "UI_AvatarIcon_Side_Wriothesley.png",
            GachaCard = "UI_Gacha_AvatarIcon_Wriothesley.png",
            GachaSplash = "UI_Gacha_AvatarImg_Wriothesley.png",
            SortId = 72,
            BeginTime = "2023-10-17T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 208600,
                    CharacterId = 10000086,
                    Name = "凛冬夜啸",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000086),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000087,
            Name = "那维莱特",
            Title = "谕告的潮音",
            Rarity = 5,
            Gender = 0,
            Element = ElementType.Water,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Neuvillette_Card.png",
            FaceIcon = "UI_AvatarIcon_Neuvillette.png",
            SideIcon = "UI_AvatarIcon_Side_Neuvillette.png",
            GachaCard = "UI_Gacha_AvatarIcon_Neuvillette.png",
            GachaSplash = "UI_Gacha_AvatarImg_Neuvillette.png",
            SortId = 71,
            BeginTime = "2023-09-25T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 208700,
                    CharacterId = 10000087,
                    Name = "澄流正裁",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000087),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000088,
            Name = "夏洛蒂",
            Title = "朗镜索真",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Ice,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Charlotte_Card.png",
            FaceIcon = "UI_AvatarIcon_Charlotte.png",
            SideIcon = "UI_AvatarIcon_Side_Charlotte.png",
            GachaCard = "UI_Gacha_AvatarIcon_Charlotte.png",
            GachaSplash = "UI_Gacha_AvatarImg_Charlotte.png",
            SortId = 73,
            BeginTime = "2023-11-06T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 208800,
                    CharacterId = 10000088,
                    Name = "「皆在镜前」",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = "",
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000088),
        };
        yield return new SnapCharacterInfo()
        {
            Id = 10000089,
            Name = "芙宁娜",
            Title = "不休独舞",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Water,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Furina_Card.png",
            FaceIcon = "UI_AvatarIcon_Furina.png",
            SideIcon = "UI_AvatarIcon_Side_Furina.png",
            GachaCard = "UI_Gacha_AvatarIcon_Furina.png",
            GachaSplash = "UI_Gacha_AvatarImg_Furina.png",
            SortId = 74,
            BeginTime = "2023-11-06T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 208900,
                    CharacterId = 10000089,
                    Name = "冠笄伶优",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = "",
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000089),
        };

        yield return new SnapCharacterInfo()
        {
            Id = 10000090,
            Name = "夏沃蕾",
            Title = "明律决罚",
            Rarity = 4,
            Gender = 1,
            Element = ElementType.Fire,
            WeaponType = WeaponType.Polearm,
            Card = "UI_AvatarIcon_Chevreuse_Card.png",
            FaceIcon = "UI_AvatarIcon_Chevreuse.png",
            SideIcon = "UI_AvatarIcon_Side_Chevreuse.png",
            GachaCard = "UI_Gacha_AvatarIcon_Chevreuse.png",
            GachaSplash = "UI_Gacha_AvatarImg_Chevreuse.png",
            SortId = 76,
            BeginTime = "2024-01-09T10:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 209000,
                    CharacterId = 10000090,
                    Name = "守正之枪",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = "",
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000090),
        };

        yield return new SnapCharacterInfo()
        {
            Id = 10000091,
            Name = "娜维娅",
            Title = "明花蔓舵",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Rock,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Navia_Card.png",
            FaceIcon = "UI_AvatarIcon_Navia.png",
            SideIcon = "UI_AvatarIcon_Side_Navia.png",
            GachaCard = "UI_Gacha_AvatarIcon_Navia.png",
            GachaSplash = "UI_Gacha_AvatarImg_Navia.png",
            SortId = 77,
            BeginTime = "2023-12-18T16:00:00.00000000Z".ParseExactDateTime(),
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 209100,
                    CharacterId = 10000091,
                    Name = "黄丝绒沙龙",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = "",
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000091),
        };

        yield return new SnapCharacterInfo()
        {
            Id = 10000092,
            Name = "嘉明",
            Title = "?",
            Rarity = 4,
            Gender = 0,
            Element = ElementType.Fire,
            WeaponType = WeaponType.Claymore,
            Card = "UI_AvatarIcon_Gaming_Card.png",
            FaceIcon = "UI_AvatarIcon_Gaming.png",
            SideIcon = "UI_AvatarIcon_Side_Gaming.png",
            GachaCard = "UI_Gacha_AvatarIcon_Gaming.png",
            GachaSplash = "UI_Gacha_AvatarImg_Gaming.png",
            SortId = 78,
            BeginTime = DateTime.Now,
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = -1,
                    CharacterId = 10000092,
                    Name = "?",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = "",
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000092),
        };

        yield return new SnapCharacterInfo()
        {
            Id = 10000093,
            Name = "闲云",
            Title = "?",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Wind,
            WeaponType = WeaponType.Catalyst,
            Card = "UI_AvatarIcon_Liuyun_Card.png",
            FaceIcon = "UI_AvatarIcon_Liuyun.png",
            SideIcon = "UI_AvatarIcon_Side_Liuyun.png",
            GachaCard = "UI_Gacha_AvatarIcon_Liuyun.png",
            GachaSplash = "UI_Gacha_AvatarImg_Liuyun.png",
            SortId = 79,
            BeginTime = DateTime.Now,
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = -1,
                    CharacterId = 10000093,
                    Name = "?",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = "",
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000093),
        };

        yield return new SnapCharacterInfo()
        {
            Id = 10000094,
            Name = "千织",
            Title = "?",
            Rarity = 5,
            Gender = 1,
            Element = ElementType.Geo,
            WeaponType = WeaponType.Sword,
            Card = "UI_AvatarIcon_Chiori_Card.png",
            FaceIcon = "UI_AvatarIcon_Chiori.png",
            SideIcon = "UI_AvatarIcon_Side_Chiori.png",
            GachaCard = "UI_Gacha_AvatarIcon_Chiori.png",
            GachaSplash = "UI_Gacha_AvatarImg_Chiori.png",
            SortId = 80,
            BeginTime = DateTime.Now,
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = -1,
                    CharacterId = 10000094,
                    Name = "?",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = "",
                },
            },
            TextureOverride = ReShadeIniMapper.Map(10000094),
        };

        ///

        yield return new SnapCharacterInfo()
        {
            Id = -1,
            Name = "奧兹",
            Rarity = 4,
            Gender = 2,
            Element = ElementType.None,
            WeaponType = WeaponType.None,
            FaceIcon = "UI_AvatarIcon_Aozi.png",
            SortId = int.MaxValue,
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 0,
                    CharacterId = -1,
                    Name = "",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "UI_AvatarIcon_Aozi.png",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(-1),
        };
        yield return new SnapCharacterInfo()
        {
            Id = -2,
            Name = "派蒙",
            Rarity = 1,
            Gender = 2,
            Element = ElementType.None,
            WeaponType = WeaponType.None,
            FaceIcon = "UI_AvatarIcon_Paimon.png",
            SortId = -2,
            Outfits = new List<SnapCharacterOutfit>
            {
                new SnapCharacterOutfit()
                {
                    Id = 0,
                    CharacterId = -2,
                    Name = "",
                    IsDefault = true,
                    Card = "",
                    FaceIcon = "UI_AvatarIcon_Paimon.png",
                    SideIcon = "",
                    GachaSplash = "",
                    TextureOverride = null,
                },
            },
            TextureOverride = ReShadeIniMapper.Map(-2),
        };
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fischless.Models;

internal static class LocalAvatars
{
    public const string UI_Gcg_Char_AvatarIcon_ALbedo = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_ALbedo.png";
    public const string UI_Gcg_Char_AvatarIcon_Amber = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Amber.png";
    public const string UI_Gcg_Char_AvatarIcon_Ayaka = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Ayaka.png";
    public const string UI_Gcg_Char_AvatarIcon_Ayato = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Ayato.png";
    public const string UI_Gcg_Char_AvatarIcon_Barbara = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Barbara.png";
    public const string UI_Gcg_Char_AvatarIcon_Beidou = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Beidou.png";
    public const string UI_Gcg_Char_AvatarIcon_Bennett = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Bennett.png";
    public const string UI_Gcg_Char_AvatarIcon_Candace = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Candace.png";
    public const string UI_Gcg_Char_AvatarIcon_Chongyun = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Chongyun.png";
    public const string UI_Gcg_Char_AvatarIcon_Collei = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Collei.png";
    public const string UI_Gcg_Char_AvatarIcon_Cyno = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Cyno.png";
    public const string UI_Gcg_Char_AvatarIcon_Dehya = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Dehya.png";
    public const string UI_Gcg_Char_AvatarIcon_Diluc = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Diluc.png";
    public const string UI_Gcg_Char_AvatarIcon_Diona = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Diona.png";
    public const string UI_Gcg_Char_AvatarIcon_Eula = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Eula.png";
    public const string UI_Gcg_Char_AvatarIcon_Feiyan = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Feiyan.png";
    public const string UI_Gcg_Char_AvatarIcon_Fischl = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Fischl.png";
    public const string UI_Gcg_Char_AvatarIcon_Ganyu = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Ganyu.png";
    public const string UI_Gcg_Char_AvatarIcon_Hutao = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Hutao.png";
    public const string UI_Gcg_Char_AvatarIcon_Itto = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Itto.png";
    public const string UI_Gcg_Char_AvatarIcon_Kaeya = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Kaeya.png";
    public const string UI_Gcg_Char_AvatarIcon_Kazuha = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Kazuha.png";
    public const string UI_Gcg_Char_AvatarIcon_Keqing = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Keqing.png";
    public const string UI_Gcg_Char_AvatarIcon_Klee = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Klee.png";
    public const string UI_Gcg_Char_AvatarIcon_Kokomi = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Kokomi.png";
    public const string UI_Gcg_Char_AvatarIcon_Lisa = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Lisa.png";
    public const string UI_Gcg_Char_AvatarIcon_Mona = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Mona.png";
    public const string UI_Gcg_Char_AvatarIcon_Nahida = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Nahida.png";
    public const string UI_Gcg_Char_AvatarIcon_Ningguang = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Ningguang.png";
    public const string UI_Gcg_Char_AvatarIcon_Noel = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Noel.png";
    public const string UI_Gcg_Char_AvatarIcon_Qin = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Qin.png";
    public const string UI_Gcg_Char_AvatarIcon_Qiqi = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Qiqi.png";
    public const string UI_Gcg_Char_AvatarIcon_Razor = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Razor.png";
    public const string UI_Gcg_Char_AvatarIcon_Sara = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Sara.png";
    public const string UI_Gcg_Char_AvatarIcon_Shenhe = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Shenhe.png";
    public const string UI_Gcg_Char_AvatarIcon_Shougun = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Shougun.png";
    public const string UI_Gcg_Char_AvatarIcon_Sucrose = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Sucrose.png";
    public const string UI_Gcg_Char_AvatarIcon_Tartaglia = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Tartaglia.png";
    public const string UI_Gcg_Char_AvatarIcon_Tighnari = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Tighnari.png";
    public const string UI_Gcg_Char_AvatarIcon_Venti = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Venti.png";
    public const string UI_Gcg_Char_AvatarIcon_Wanderer = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Wanderer.png";
    public const string UI_Gcg_Char_AvatarIcon_Xiangling = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Xiangling.png";
    public const string UI_Gcg_Char_AvatarIcon_Xiao = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Xiao.png";
    public const string UI_Gcg_Char_AvatarIcon_Xingqiu = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Xingqiu.png";
    public const string UI_Gcg_Char_AvatarIcon_Yae = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Yae.png";
    public const string UI_Gcg_Char_AvatarIcon_Yaoyao = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Yaoyao.png";
    public const string UI_Gcg_Char_AvatarIcon_Yoimiya = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Yoimiya.png";
    public const string UI_Gcg_Char_AvatarIcon_Zhongli = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_AvatarIcon_Zhongli.png";
    public const string UI_Gcg_Char_EnemyIcon_AbyssEle = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_AbyssEle.png";
    public const string UI_Gcg_Char_EnemyIcon_AbyssFire = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_AbyssFire.png";
    public const string UI_Gcg_Char_EnemyIcon_AbyssIce = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_AbyssIce.png";
    public const string UI_Gcg_Char_EnemyIcon_AbyssWater = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_AbyssWater.png";
    public const string UI_Gcg_Char_EnemyIcon_BruteAxeElec = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_BruteAxeElec.png";
    public const string UI_Gcg_Char_EnemyIcon_BruteAxeFire = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_BruteAxeFire.png";
    public const string UI_Gcg_Char_EnemyIcon_BruteIceShield = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_BruteIceShield.png";
    public const string UI_Gcg_Char_EnemyIcon_BruteRockShield = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_BruteRockShield.png";
    public const string UI_Gcg_Char_EnemyIcon_BruteShield = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_BruteShield.png";
    public const string UI_Gcg_Char_EnemyIcon_Hili = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_Hili.png";
    public const string UI_Gcg_Char_EnemyIcon_HiliClub = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_HiliClub.png";
    public const string UI_Gcg_Char_EnemyIcon_HiliIce = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_HiliIce.png";
    public const string UI_Gcg_Char_EnemyIcon_HiliRange = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_HiliRange.png";
    public const string UI_Gcg_Char_EnemyIcon_HiliRangeElec = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_HiliRangeElec.png";
    public const string UI_Gcg_Char_EnemyIcon_KairagiElec = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_KairagiElec.png";
    public const string UI_Gcg_Char_EnemyIcon_KairagiFire = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_KairagiFire.png";
    public const string UI_Gcg_Char_EnemyIcon_Rockdog = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_Rockdog.png";
    public const string UI_Gcg_Char_EnemyIcon_SamuraiRonin01 = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_SamuraiRonin01.png";
    public const string UI_Gcg_Char_EnemyIcon_SamuraiRonin02 = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_SamuraiRonin02.png";
    public const string UI_Gcg_Char_EnemyIcon_SamuraiRonin03 = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_SamuraiRonin03.png";
    public const string UI_Gcg_Char_EnemyIcon_ShamanGrass = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_ShamanGrass.png";
    public const string UI_Gcg_Char_EnemyIcon_ShamanRock = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_ShamanRock.png";
    public const string UI_Gcg_Char_EnemyIcon_ShamanWater = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_ShamanWater.png";
    public const string UI_Gcg_Char_EnemyIcon_ShamanWind = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_ShamanWind.png";
    public const string UI_Gcg_Char_EnemyIcon_SkirmisherIce = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_SkirmisherIce.png";
    public const string UI_Gcg_Char_EnemyIcon_Skirnisherfatfire = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_Skirnisherfatfire.png";
    public const string UI_Gcg_Char_EnemyIcon_Skirnisherfatrock = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_Skirnisherfatrock.png";
    public const string UI_Gcg_Char_EnemyIcon_Skirnisherstrongele = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_Skirnisherstrongele.png";
    public const string UI_Gcg_Char_EnemyIcon_SlimeElec = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_SlimeElec.png";
    public const string UI_Gcg_Char_EnemyIcon_SlimeWater = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_SlimeWater.png";
    public const string UI_Gcg_Char_EnemyIcon_UnDeltaIce = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_UnDeltaIce.png";
    public const string UI_Gcg_Char_EnemyIcon_UnDeltaRock = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_UnDeltaRock.png";
    public const string UI_Gcg_Char_EnemyIcon_UnDeltaWater = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_UnDeltaWater.png";
    public const string UI_Gcg_Char_EnemyIcon_UnuAnudattaGrass = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_EnemyIcon_UnuAnudattaGrass.png";
    public const string UI_Gcg_Char_MonsterIcon_Bruterock = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_MonsterIcon_Bruterock.png";
    public const string UI_Gcg_Char_MonsterIcon_EffigyElectric = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_MonsterIcon_EffigyElectric.png";
    public const string UI_Gcg_Char_MonsterIcon_Fatuus = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_MonsterIcon_Fatuus.png";
    public const string UI_Gcg_Char_MonsterIcon_FatuusMageIce = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_MonsterIcon_FatuusMageIce.png";
    public const string UI_Gcg_Char_MonsterIcon_Fungusgrass = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_MonsterIcon_Fungusgrass.png";
    public const string UI_Gcg_Char_MonsterIcon_InvokerDeaconFire = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_MonsterIcon_InvokerDeaconFire.png";
    public const string UI_Gcg_Char_MonsterIcon_Maidenwater = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_MonsterIcon_Maidenwater.png";
    public const string UI_Gcg_Char_MonsterIcon_Ningyo = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_MonsterIcon_Ningyo.png";
    public const string UI_Gcg_Char_MonsterIcon_Oceanid = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_MonsterIcon_Oceanid.png";
    public const string UI_Gcg_Char_MonsterIcon_SkirmisherWater = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_MonsterIcon_SkirmisherWater.png";
    public const string UI_Gcg_Char_MonsterIcon_SkirmisherWind = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_Gcg_Char_MonsterIcon_SkirmisherWind.png";
    public const string UI_MusicV3SelectPage_Album_Pic01 = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_MusicV3SelectPage_Album_Pic01.png";
    public const string UI_MusicV3SelectPage_Album_Pic02 = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_MusicV3SelectPage_Album_Pic02.png";
    public const string UI_MusicV3SelectPage_Album_Pic03 = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_MusicV3SelectPage_Album_Pic03.png";
    public const string UI_MusicV3SelectPage_Album_Pic04 = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_MusicV3SelectPage_Album_Pic04.png";
    public const string UI_MusicV3SelectPage_Album_Pic05 = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_MusicV3SelectPage_Album_Pic05.png";
    public const string UI_MusicV3SelectPage_Album_Pic06 = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_MusicV3SelectPage_Album_Pic06.png";
    public const string UI_MusicV3SelectPage_Album_Pic07 = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_MusicV3SelectPage_Album_Pic07.png";
    public const string UI_MusicV3_Album_09_Small_Round = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_MusicV3_Album_09_Small_Round.png";
    public const string UI_MusicV3_Album_10_Small_Round = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_MusicV3_Album_10_Small_Round.png";
    public const string UI_MusicV3_Album_11_Small_Round = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_MusicV3_Album_11_Small_Round.png";
    public const string UI_MusicV3_Album_12_Small_Round = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_MusicV3_Album_12_Small_Round.png";
    public const string UI_MusicV3_Album_13_Small_Round = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_MusicV3_Album_13_Small_Round.png";
    public const string UI_MusicV3_Album_14_Small_Round = "pack://application:,,,/Fischless;component/Assets/Images/LocalAvatars/UI_MusicV3_Album_14_Small_Round.png";

    public static readonly Dictionary<string, string> Stocks = new();
    public static string Default => UI_Gcg_Char_AvatarIcon_Fischl;

    static LocalAvatars()
    {
        FieldInfo[] fields = typeof(LocalAvatars).GetFields();

        foreach (FieldInfo field in fields)
        {
            if (field.IsStatic && field.FieldType == typeof(string))
            {
                Stocks.Add(field.Name, field.GetValue(null) as string ?? throw new Exception());
            }
        }
    }
}

﻿using Fischless.Fetch.Datas;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fischless.Models;

internal static class ReShadeAvatars
{
    public const string UI_AvatarIcon_Albedo = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Albedo.png";
    public const string UI_AvatarIcon_Albedo_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Albedo_Card.png";
    public const string UI_AvatarIcon_Alhatham = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Alhatham.png";
    public const string UI_AvatarIcon_Alhatham_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Alhatham_Card.png";
    public const string UI_AvatarIcon_Aloy = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Aloy.png";
    public const string UI_AvatarIcon_Aloy_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Aloy_Card.png";
    public const string UI_AvatarIcon_Ambor = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Ambor.png";
    public const string UI_AvatarIcon_Ambor_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Ambor_Card.png";
    public const string UI_AvatarIcon_Aozi = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Aozi.png";
    public const string UI_AvatarIcon_Ayaka = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Ayaka.png";
    public const string UI_AvatarIcon_AyakaCostumeFruhling = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_AyakaCostumeFruhling.png";
    public const string UI_AvatarIcon_Ayaka_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Ayaka_Card.png";
    public const string UI_AvatarIcon_Ayato = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Ayato.png";
    public const string UI_AvatarIcon_Ayato_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Ayato_Card.png";
    public const string UI_AvatarIcon_Baizhuer = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Baizhuer.png";
    public const string UI_AvatarIcon_Baizhuer_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Baizhuer_Card.png";
    public const string UI_AvatarIcon_Barbara = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Barbara.png";
    public const string UI_AvatarIcon_BarbaraCostumeSummertime = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_BarbaraCostumeSummertime.png";
    public const string UI_AvatarIcon_Barbara_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Barbara_Card.png";
    public const string UI_AvatarIcon_Beidou = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Beidou.png";
    public const string UI_AvatarIcon_Beidou_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Beidou_Card.png";
    public const string UI_AvatarIcon_Bennett = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Bennett.png";
    public const string UI_AvatarIcon_Bennett_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Bennett_Card.png";
    public const string UI_AvatarIcon_Candace = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Candace.png";
    public const string UI_AvatarIcon_Candace_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Candace_Card.png";
    public const string UI_AvatarIcon_Chongyun = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Chongyun.png";
    public const string UI_AvatarIcon_Chongyun_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Chongyun_Card.png";
    public const string UI_AvatarIcon_Collei = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Collei.png";
    public const string UI_AvatarIcon_Collei_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Collei_Card.png";
    public const string UI_AvatarIcon_Cyno = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Cyno.png";
    public const string UI_AvatarIcon_Cyno_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Cyno_Card.png";
    public const string UI_AvatarIcon_Dehya = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Dehya.png";
    public const string UI_AvatarIcon_Dehya_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Dehya_Card.png";
    public const string UI_AvatarIcon_Diluc = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Diluc.png";
    public const string UI_AvatarIcon_DilucCostumeFlamme = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_DilucCostumeFlamme.png";
    public const string UI_AvatarIcon_Diluc_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Diluc_Card.png";
    public const string UI_AvatarIcon_Diona = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Diona.png";
    public const string UI_AvatarIcon_Diona_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Diona_Card.png";
    public const string UI_AvatarIcon_Dori = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Dori.png";
    public const string UI_AvatarIcon_Dori_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Dori_Card.png";
    public const string UI_AvatarIcon_Eula = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Eula.png";
    public const string UI_AvatarIcon_Eula_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Eula_Card.png";
    public const string UI_AvatarIcon_Faruzan = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Faruzan.png";
    public const string UI_AvatarIcon_Faruzan_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Faruzan_Card.png";
    public const string UI_AvatarIcon_Feiyan = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Feiyan.png";
    public const string UI_AvatarIcon_Feiyan_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Feiyan_Card.png";
    public const string UI_AvatarIcon_Fischl = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Fischl.png";
    public const string UI_AvatarIcon_FischlCostumeHighness = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_FischlCostumeHighness.png";
    public const string UI_AvatarIcon_Fischl_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Fischl_Card.png";
    public const string UI_AvatarIcon_Freminet = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Freminet.png";
    public const string UI_AvatarIcon_Freminet_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Freminet_Card.png";
    public const string UI_AvatarIcon_Ganyu = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Ganyu.png";
    public const string UI_AvatarIcon_Ganyu_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Ganyu_Card.png";
    public const string UI_AvatarIcon_Gorou = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Gorou.png";
    public const string UI_AvatarIcon_Gorou_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Gorou_Card.png";
    public const string UI_AvatarIcon_Heizo = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Heizo.png";
    public const string UI_AvatarIcon_Heizo_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Heizo_Card.png";
    public const string UI_AvatarIcon_Hutao = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Hutao.png";
    public const string UI_AvatarIcon_Hutao_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Hutao_Card.png";
    public const string UI_AvatarIcon_Itto = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Itto.png";
    public const string UI_AvatarIcon_Itto_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Itto_Card.png";
    public const string UI_AvatarIcon_Kaeya = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Kaeya.png";
    public const string UI_AvatarIcon_KaeyaCostumeDancer = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_KaeyaCostumeDancer.png";
    public const string UI_AvatarIcon_Kaeya_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Kaeya_Card.png";
    public const string UI_AvatarIcon_Kaveh = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Kaveh.png";
    public const string UI_AvatarIcon_Kaveh_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Kaveh_Card.png";
    public const string UI_AvatarIcon_Kazuha = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Kazuha.png";
    public const string UI_AvatarIcon_Kazuha_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Kazuha_Card.png";
    public const string UI_AvatarIcon_Keqing = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Keqing.png";
    public const string UI_AvatarIcon_KeqingCostumeFeather = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_KeqingCostumeFeather.png";
    public const string UI_AvatarIcon_Keqing_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Keqing_Card.png";
    public const string UI_AvatarIcon_Klee = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Klee.png";
    public const string UI_AvatarIcon_KleeCostumeWitch = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_KleeCostumeWitch.png";
    public const string UI_AvatarIcon_Klee_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Klee_Card.png";
    public const string UI_AvatarIcon_Kokomi = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Kokomi.png";
    public const string UI_AvatarIcon_Kokomi_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Kokomi_Card.png";
    public const string UI_AvatarIcon_Layla = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Layla.png";
    public const string UI_AvatarIcon_Layla_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Layla_Card.png";
    public const string UI_AvatarIcon_Linette = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Linette.png";
    public const string UI_AvatarIcon_Linette_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Linette_Card.png";
    public const string UI_AvatarIcon_Liney = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Liney.png";
    public const string UI_AvatarIcon_Liney_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Liney_Card.png";
    public const string UI_AvatarIcon_Lisa = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Lisa.png";
    public const string UI_AvatarIcon_LisaCostumeStudentin = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_LisaCostumeStudentin.png";
    public const string UI_AvatarIcon_LisaCostumeStudentin_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_LisaCostumeStudentin_Card.png";
    public const string UI_AvatarIcon_Lisa_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Lisa_Card.png";
    public const string UI_AvatarIcon_Mika = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Mika.png";
    public const string UI_AvatarIcon_Mika_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Mika_Card.png";
    public const string UI_AvatarIcon_Momoka = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Momoka.png";
    public const string UI_AvatarIcon_Momoka_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Momoka_Card.png";
    public const string UI_AvatarIcon_Mona = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Mona.png";
    public const string UI_AvatarIcon_Mona_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Mona_Card.png";
    public const string UI_AvatarIcon_Nahida = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Nahida.png";
    public const string UI_AvatarIcon_Nahida_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Nahida_Card.png";
    public const string UI_AvatarIcon_Neuvillette = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Neuvillette.png";
    public const string UI_AvatarIcon_Neuvillette_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Neuvillette_Card.png";
    public const string UI_AvatarIcon_Nilou = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Nilou.png";
    public const string UI_AvatarIcon_Nilou_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Nilou_Card.png";
    public const string UI_AvatarIcon_Ningguang = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Ningguang.png";
    public const string UI_AvatarIcon_NingguangCostumeFloral = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_NingguangCostumeFloral.png";
    public const string UI_AvatarIcon_Ningguang_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Ningguang_Card.png";
    public const string UI_AvatarIcon_Noel = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Noel.png";
    public const string UI_AvatarIcon_Noel_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Noel_Card.png";
    public const string UI_AvatarIcon_Paimon = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Paimon.png";
    public const string UI_AvatarIcon_PlayerBoy = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_PlayerBoy.png";
    public const string UI_AvatarIcon_PlayerBoy_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_PlayerBoy_Card.png";
    public const string UI_AvatarIcon_PlayerGirl = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_PlayerGirl.png";
    public const string UI_AvatarIcon_PlayerGirl_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_PlayerGirl_Card.png";
    public const string UI_AvatarIcon_Qin = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Qin.png";
    public const string UI_AvatarIcon_QinCostumeSea = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_QinCostumeSea.png";
    public const string UI_AvatarIcon_Qin_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Qin_Card.png";
    public const string UI_AvatarIcon_Qiqi = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Qiqi.png";
    public const string UI_AvatarIcon_Qiqi_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Qiqi_Card.png";
    public const string UI_AvatarIcon_Razor = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Razor.png";
    public const string UI_AvatarIcon_Razor_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Razor_Card.png";
    public const string UI_AvatarIcon_Rosaria = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Rosaria.png";
    public const string UI_AvatarIcon_Rosaria_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Rosaria_Card.png";
    public const string UI_AvatarIcon_Sara = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Sara.png";
    public const string UI_AvatarIcon_Sara_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Sara_Card.png";
    public const string UI_AvatarIcon_Sayu = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Sayu.png";
    public const string UI_AvatarIcon_Sayu_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Sayu_Card.png";
    public const string UI_AvatarIcon_Shenhe = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Shenhe.png";
    public const string UI_AvatarIcon_Shenhe_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Shenhe_Card.png";
    public const string UI_AvatarIcon_Shinobu = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Shinobu.png";
    public const string UI_AvatarIcon_Shinobu_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Shinobu_Card.png";
    public const string UI_AvatarIcon_Shougun = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Shougun.png";
    public const string UI_AvatarIcon_Shougun_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Shougun_Card.png";
    public const string UI_AvatarIcon_Sucrose = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Sucrose.png";
    public const string UI_AvatarIcon_Sucrose_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Sucrose_Card.png";
    public const string UI_AvatarIcon_Tartaglia = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Tartaglia.png";
    public const string UI_AvatarIcon_Tartaglia_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Tartaglia_Card.png";
    public const string UI_AvatarIcon_Tighnari = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Tighnari.png";
    public const string UI_AvatarIcon_Tighnari_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Tighnari_Card.png";
    public const string UI_AvatarIcon_Tohma = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Tohma.png";
    public const string UI_AvatarIcon_Tohma_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Tohma_Card.png";
    public const string UI_AvatarIcon_Venti = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Venti.png";
    public const string UI_AvatarIcon_Venti_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Venti_Card.png";
    public const string UI_AvatarIcon_Wanderer = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Wanderer.png";
    public const string UI_AvatarIcon_Wanderer_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Wanderer_Card.png";
    public const string UI_AvatarIcon_Wriothesley = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Wriothesley.png";
    public const string UI_AvatarIcon_Wriothesley_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Wriothesley_Card.png";
    public const string UI_AvatarIcon_Xiangling = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Xiangling.png";
    public const string UI_AvatarIcon_Xiangling_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Xiangling_Card.png";
    public const string UI_AvatarIcon_Xiao = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Xiao.png";
    public const string UI_AvatarIcon_Xiao_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Xiao_Card.png";
    public const string UI_AvatarIcon_Xingqiu = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Xingqiu.png";
    public const string UI_AvatarIcon_Xingqiu_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Xingqiu_Card.png";
    public const string UI_AvatarIcon_Xinyan = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Xinyan.png";
    public const string UI_AvatarIcon_Xinyan_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Xinyan_Card.png";
    public const string UI_AvatarIcon_Yae = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Yae.png";
    public const string UI_AvatarIcon_Yae_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Yae_Card.png";
    public const string UI_AvatarIcon_Yaoyao = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Yaoyao.png";
    public const string UI_AvatarIcon_Yaoyao_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Yaoyao_Card.png";
    public const string UI_AvatarIcon_Yelan = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Yelan.png";
    public const string UI_AvatarIcon_Yelan_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Yelan_Card.png";
    public const string UI_AvatarIcon_Yoimiya = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Yoimiya.png";
    public const string UI_AvatarIcon_Yoimiya_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Yoimiya_Card.png";
    public const string UI_AvatarIcon_Yunjin = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Yunjin.png";
    public const string UI_AvatarIcon_Yunjin_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Yunjin_Card.png";
    public const string UI_AvatarIcon_Zhongli = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Zhongli.png";
    public const string UI_AvatarIcon_Zhongli_Card = "pack://application:,,,/Fischless;component/Assets/Images/AvatarIcons/UI_AvatarIcon_Zhongli_Card.png";
    
    public static readonly Dictionary<string, string> Stocks = new();
    public static readonly List<ReShadeAvatar> Avatars = new();

    static ReShadeAvatars()
    {
        FieldInfo[] fields = typeof(ReShadeAvatars).GetFields();

        foreach (FieldInfo field in fields)
        {
            if (field.Name.EndsWith("_Card") || field.Name.Contains("Costume"))
            {
                continue;
            }
            if (field.IsStatic && field.FieldType == typeof(string))
            {
                Stocks.Add(field.Name, field.GetValue(null) as string ?? throw new Exception());
            }
        }

        foreach (var stock in Stocks.Values)
        {
            string avatarIconName = stock.Split('/')[^1].Replace(".png", string.Empty);

            Avatars.Add(new ReShadeAvatar()
            {
                AvatarIcon = stock,
                RarityBackground = $"pack://application:,,,/Fischless;component/Assets/Images/Rarity_{UnuselessDataProvider.GetAvatarRarity(avatarIconName)}_Background.png",
                NameKey = $"AvatarNameOf{avatarIconName.Replace("UI_AvatarIcon_", string.Empty)}",
            });
        }
    }
}

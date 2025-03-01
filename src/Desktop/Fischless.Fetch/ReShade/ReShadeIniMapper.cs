﻿namespace Fischless.Fetch.ReShade;

public static class ReShadeIniMapper
{
    public static string Map(int id)
    {
        return id switch
        {
            10000002 => "Ayaka;Linghua;KamisatoAyaka;AyakaKamisato",
            10000003 => "Jean;Qin",
            10000005 => "TravelerBoy;TravelerMale;Nanzhu",
            10000006 => "Lisa;Lisha",
            10000007 => "TravelerGirl;TravelerFemale;Nvzhu",
            10000014 => "Barbara;Babara;Babala",
            10000015 => "Kaeya;Kaiya",
            10000016 => "Diluc;Diluke",
            10000020 => "Razor;Leize",
            10000021 => "Ambor;Amber;Anbo",
            10000022 => "Venti;Wendi",
            10000023 => "Xiangling",
            10000024 => "Beidou",
            10000025 => "Xingqiu",
            10000026 => "Xiao",
            10000027 => "Ningguang",
            10000029 => "Klee;Keli",
            10000030 => "Zhongli",
            10000031 => "Fischl;Feixieer",
            10000032 => "Bennett;Bannite",
            10000033 => "Tartaglia;Childe;Gongzi",
            10000034 => "Noel;Noelle;Nuoaier",
            10000035 => "Qiqi;Nana",
            10000036 => "Chongyun",
            10000037 => "Ganyu",
            10000038 => "Albedo;Abeiduo",
            10000039 => "Diona;Diaona",
            10000041 => "Mona",
            10000042 => "Keqing",
            10000043 => "Sucrose;Shatan",
            10000044 => "Xinyan",
            10000045 => "Rosaria;Luoshaliya",
            10000046 => "Hutao",
            10000047 => "Kazuha;Yiye;KaedeharaKazuha;KazuhaKaedehara",
            10000048 => "Feiyan;Yanfei",
            10000049 => "Yoimiya",
            10000050 => "Tohma;Thoma",
            10000051 => "Eula;Youla",
            10000052 => "Shougun;Raiden;Jiangjun;Leidian",
            10000053 => "Sayu;Zaoyou",
            10000054 => "Kokomi;Xinhai",
            10000055 => "Gorou;Wulang",
            10000056 => "Sara;KujouSara;Sala",
            10000057 => "Itto;Yidou;AratakiItto;IttoArataki",
            10000058 => "Yae;Bachong;Shenzi",
            10000059 => "Heizo;Pingzang",
            10000060 => "Yelan",
            10000061 => "Kirara;Momoka;QiLiangLiang",
            10000062 => "Aloy",
            10000063 => "Shenhe",
            10000064 => "Yunjin",
            10000065 => "Shinobu;KukiShinobu",
            10000066 => "Ayato;Linren;KamisatoAyato;AyatoKamisato",
            10000067 => "Collei;Kelai",
            10000068 => "Dori;Duoli",
            10000069 => "Tighnari;Tinali",
            10000070 => "Nilou;Nilu;Nirou",
            10000071 => "Cyno;Saino",
            10000072 => "Candace;Kandisi",
            10000073 => "Nahida;Naxida",
            10000074 => "Layla;Laila",
            10000075 => "Wanderer;Liulanzhe;Scara",
            10000076 => "Faruzan;Falusan",
            10000077 => "Yaoyao",
            10000078 => "Alhatham;Alhaitham",
            10000079 => "Dehya;Dixiya",
            10000080 => "Mika",
            10000081 => "Kaveh;Kawei",
            10000082 => "Baizhuer;Baizhu",
            10000083 => "Linette;Lynette;Linnite",
            10000084 => "Liney;Lyney;Linni$",
            10000085 => "Freminet;Feimini",
            10000086 => "Wriothesley;Laiousili",
            10000087 => "Neuvillette;Naweilaite",
            10000088 => "Charlotte;Xialuodi",
            10000089 => "Furina;Focalor;Funingna",
            10000090 => "Chevreuse;Xiawolei",
            10000091 => "Navia;Naweiya",
            10000092 => "Gaming;Jiaming",
            10000093 => "Liuyun;Xianyun",
            10000094 => "Chiori;Qianzhi",
            10000095 => "Sigewinne;Xigewen",
            10000096 => "Arlecchino;Aleiqinuo",
            10000097 => "Sethos;Saisuosi",
            10000098 => "Clorinde;Keluolinde",
            10000099 => "Emilie;Aimeiliai",
            10000100 => "Kachina;Kaqina",
            10000101 => "Kinich;Jiniqi",
            10000102 => "Mualani;Malani",
            10000103 => "Xilonen;Xinuoning",
            10000104 => "Chasca;Qiasika",
            10000105 => "Ororon;Olorun;Ouluolun",
            10000106 => "Mavuika;Maweika",
            10000107 => "Citlali;Xitelali",
            10000108 => "Lanyan",
            10000109 => "Mizuki;YumemizukiMizuki;Ruixi",
            -2 => "Paimon",
            -1 or _ => "Aozi",
        };
    }
}

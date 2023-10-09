namespace Fischless.Fetch.Datas.Core;

[Flags]
public enum ElementType
{
    None = 0,

    Physics = 1,

    Pyro = 2,
    Fire = 2,

    Hydro = 4,
    Water = 4,

    Anemo = 8,
    Wind = 8,

    Electro = 16,

    Dendro = 32,
    Grass = 32,

    Cryo = 64,
    Ice = 64,

    Geo = 128,
    Rock = 128,
}

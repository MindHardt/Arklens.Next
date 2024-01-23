namespace Arklens.Next.Entities;

/// <summary>
/// Contains info about race impact on characteristics.
/// </summary>
/// <param name="Str">Strength</param>
/// <param name="Dex">Dexterity</param>
/// <param name="Con">Constitution</param>
/// <param name="Int">Intelligence</param>
/// <param name="Wis">Wisdom</param>
/// <param name="Cha">Charisma</param>
public readonly record struct RacialCharacteristicImpact(
    RaceImpact Str = RaceImpact.Unaffected,
    RaceImpact Dex = RaceImpact.Unaffected,
    RaceImpact Con = RaceImpact.Unaffected,
    RaceImpact Int = RaceImpact.Unaffected,
    RaceImpact Wis = RaceImpact.Unaffected,
    RaceImpact Cha = RaceImpact.Unaffected)
{
    public RacialCharacteristicImpact(int Str = 0, int Dex = 0, int Con = 0, int Int = 0, int Wis = 0, int Cha = 0) : this(
            (RaceImpact)Str,
            (RaceImpact)Dex,
            (RaceImpact)Con,
            (RaceImpact)Int,
            (RaceImpact)Wis,
            (RaceImpact)Cha)
    { }
}

public enum RaceImpact : sbyte
{
    Increased = +2,
    Unaffected = 0,
    Decreased = -2
}
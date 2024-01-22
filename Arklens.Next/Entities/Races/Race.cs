using System.Globalization;
using System.Runtime.CompilerServices;
using Arklens.Next.Core;
using Arklens.Next.Entities.Traits;
using EnumerationGenerator;
using ResourcesGenerator;

namespace Arklens.Next.Entities.Races;

[AlidDomain]
[GenerateEnumeration]
public partial record Race : AlidEntity
{
    private readonly LocalizationFactory _localizationFactory;
    public override string GetLocalizedName(CultureInfo? cultureInfo = null) => _localizationFactory(cultureInfo);

    public required RaceTraits Traits { get; init; }
    public required RacialCharacteristicImpact? CharacteristicImpact { get; init; }

    private Race([CallerMemberName] string ownName = "") : base(ownName)
    {
        _localizationFactory = RaceResources.Find(ownName);
    }

    public static Race Human { get; } = new()
    {
        Traits = (Trait.MindFlexibility, Trait.Handyman),
        CharacteristicImpact = null
    };

    public static Race Elf { get; } = new()
    {
        Traits = (Trait.Perception, Trait.Insomnia),
        CharacteristicImpact = new RacialCharacteristicImpact(Dex: +2, Int: +2, Con: -2)
    };

    public static Race Dwarf { get; } = new()
    {
        Traits = (Trait.RockHard, Trait.SlowButSteady),
        CharacteristicImpact = new RacialCharacteristicImpact(Con: +2, Wis: +2, Cha: -2)
    };

    public static Race Kitsune { get; } = new()
    {
        Traits = (Trait.WillToLive, Trait.BeastFolk),
        CharacteristicImpact = new RacialCharacteristicImpact(Dex: +2, Cha: +2, Str: -2)
    };

    public static Race Minas { get; } = new()
    {
        Traits = (Trait.SecondWind, Trait.PlainsWalk),
        CharacteristicImpact = new RacialCharacteristicImpact(Str: +2, Con: +2, Int: -2)
    };

    public static Race Serpent { get; } = new()
    {
        Traits = (Trait.Amphibious, Trait.Scales),
        CharacteristicImpact = new RacialCharacteristicImpact(Con: +2, Int: +2, Wis: -2)
    };
}
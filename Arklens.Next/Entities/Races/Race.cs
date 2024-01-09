using System.Runtime.CompilerServices;
using Arklens.Next.Core;
using Arklens.Next.Entities.Traits;
using EnumerationGenerator;
using SourceGeneratedAlidSearchGenerator;

namespace Arklens.Next.Entities.Races;

[AlidDomain]
[SearchInclude]
[GenerateEnumeration]
public partial record Race : IAlidEntity
{
    public RaceTraits Traits { get; }
    public AlidName OwnName { get; }

    private Race(RaceTraits raceTraits, [CallerMemberName] string ownName = "")
    {
        Traits = raceTraits;
        OwnName = AlidName.Create(ownName);
    }

    public static Race Human { get; } = new((Trait.MindFlexibility, Trait.Handyman));
    public static Race Elf { get; } = new((Trait.Perception, Trait.Insomnia));
    public static Race Dwarf { get; } = new((Trait.RockHard, Trait.SlowButSteady));
    public static Race Kitsune { get; } = new((Trait.WillToLive, Trait.BeastFolk));
    public static Race Minas { get; } = new((Trait.SecondWind, Trait.PlainsWalk));
    public static Race Serpent { get; } = new((Trait.Amphibious, Trait.Scales));
}
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using Arklens.Next.Core;
using EnumerationGenerator;
using ResourcesGenerator;

namespace Arklens.Next.Entities;

[AlidDomain]
[GenerateEnumeration]
public partial record Alignment : AlidEntity
{
    public Goodness Goodness { get; }
    public Lawfulness Lawfulness { get; }

    private Alignment(Lawfulness lawfulness, Goodness goodness, [CallerMemberName] string ownName = "") : base(ownName)
    {
        Lawfulness = lawfulness;
        Goodness = goodness;
    }

    public static Alignment LawfulGood { get; } = new(Lawfulness.Lawful, Goodness.Good);
    public static Alignment NeutralGood { get; } = new(Lawfulness.Neutral, Goodness.Good);
    public static Alignment ChaoticGood { get; } = new(Lawfulness.Chaotic, Goodness.Good);
    public static Alignment LawfulNeutral { get; } = new(Lawfulness.Lawful, Goodness.Neutral);
    public static Alignment Neutral { get; } = new(Lawfulness.Neutral, Goodness.Neutral);
    public static Alignment ChaoticNeutral { get; } = new(Lawfulness.Chaotic, Goodness.Neutral);
    public static Alignment LawfulEvil { get; } = new(Lawfulness.Lawful, Goodness.Evil);
    public static Alignment NeutralEvil { get; } = new(Lawfulness.Neutral, Goodness.Evil);
    public static Alignment ChaoticEvil { get; } = new(Lawfulness.Chaotic, Goodness.Evil);

    public override string GetLocalizedName(CultureInfo? cultureInfo = null)
    {
        if (this is { Lawfulness: Lawfulness.Neutral, Goodness: Goodness.Neutral })
        {
            return AlignmentResources.Lawfulness_Neutral(cultureInfo);
        }

        var lawfulness = Lawfulness switch
        {
            Lawfulness.Lawful => AlignmentResources.Lawfulness_Lawful(cultureInfo),
            Lawfulness.Neutral => AlignmentResources.Lawfulness_Neutral(cultureInfo),
            Lawfulness.Chaotic => AlignmentResources.Lawfulness_Chaotic(cultureInfo),
            _ => throw new UnreachableException()
        };
        var goodness = Goodness switch
        {
            Goodness.Good => AlignmentResources.Goodness_Good(cultureInfo),
            Goodness.Neutral => AlignmentResources.Goodness_Neutral(cultureInfo),
            Goodness.Evil => AlignmentResources.Goodness_Evil(cultureInfo),
            _ => throw new UnreachableException()
        };
        return $"{lawfulness} {goodness}";
    }
}

public enum Goodness : sbyte
{
    Good = 1,
    Neutral = 0,
    Evil = -1
}

public enum Lawfulness : sbyte
{
    Lawful = 1,
    Neutral = 0,
    Chaotic = -1
}
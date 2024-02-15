using System.Runtime.CompilerServices;
using Arklens.Next.Core;
using EnumerationGenerator;
using Resources.Next;
using Resources.Next.Generated;

namespace Arklens.Next.Entities;

[AlidDomain]
[GenerateEnumeration]
public partial record Alignment : LocalizedAlidEntity<AlignmentResources>
{
    public Goodness Goodness { get; }
    public Lawfulness Lawfulness { get; }

    private Alignment(Lawfulness lawfulness, Goodness goodness, [CallerMemberName] string ownName = "")
        : base(ownName)
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
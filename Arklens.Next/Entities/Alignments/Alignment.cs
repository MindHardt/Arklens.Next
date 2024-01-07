using System.Runtime.CompilerServices;
using Arklens.Entities.Alignments;
using Arklens.Next.Core;
using EnumerationGenerator;

namespace Arklens.Next.Entities.Alignments;

[AlidDomain]
[GenerateEnumeration]
public readonly partial record struct Alignment
    : IAlidEntity
{
    public static Alignment LawfulGood { get; } = new(LawfulChaotic.Lawful, GoodEvil.Good);
    public static Alignment NeutralGood { get; } = new(LawfulChaotic.Neutral, GoodEvil.Good);
    public static Alignment ChaoticGood { get; } = new(LawfulChaotic.Chaotic, GoodEvil.Good);
    public static Alignment LawfulNeutral { get; } = new(LawfulChaotic.Lawful, GoodEvil.Neutral);
    public static Alignment Neutral { get; } = new(LawfulChaotic.Neutral, GoodEvil.Neutral);
    public static Alignment ChaoticNeutral { get; } = new(LawfulChaotic.Chaotic, GoodEvil.Neutral);
    public static Alignment LawfulEvil { get; } = new(LawfulChaotic.Lawful, GoodEvil.Evil);
    public static Alignment NeutralEvil { get; } = new(LawfulChaotic.Neutral, GoodEvil.Evil);
    public static Alignment ChaoticEvil { get; } = new(LawfulChaotic.Chaotic, GoodEvil.Evil);

    public string Emoji { get; }
    public AlidName OwnName { get; }
    public GoodEvil GoodEvil { get; }
    public LawfulChaotic LawfulChaotic { get; }

    private Alignment(LawfulChaotic lawfulChaotic, GoodEvil goodEvil, [CallerMemberName] string ownName = "")
    {
        LawfulChaotic = lawfulChaotic;
        GoodEvil = goodEvil;
        Emoji = GetEmoji(lawfulChaotic, goodEvil);
        OwnName = AlidName.Create(ownName);
    }

    private static string GetEmoji(LawfulChaotic lawfulChaotic, GoodEvil goodEvil)
        => lawfulChaotic switch
        {
            LawfulChaotic.Lawful => "⚖️",
            LawfulChaotic.Neutral => "➖",
            LawfulChaotic.Chaotic => "🌬️",
            _ => "❓"
        } + goodEvil switch
        {
            GoodEvil.Good => "🙂",
            GoodEvil.Neutral => "😐",
            GoodEvil.Evil => "😠",
            _ => "❓"
        };

    public string ToLocalizedString()
        => this is { LawfulChaotic: LawfulChaotic.Neutral, GoodEvil: GoodEvil.Neutral }
            ? AlignmentResources.GoodEvil_Neutral
            : LawfulChaotic switch
            {
                LawfulChaotic.Lawful => AlignmentResources.LawfulChaotic_Lawful,
                LawfulChaotic.Neutral => AlignmentResources.LawfulChaotic_Neutral,
                LawfulChaotic.Chaotic => AlignmentResources.LawfulChaotic_Chaotic,
                _ => AlignmentResources.LawfulChaotic_Unknown
            } + @" " + GoodEvil switch
            {
                GoodEvil.Good => AlignmentResources.GoodEvil_Good,
                GoodEvil.Neutral => AlignmentResources.GoodEvil_Neutral,
                GoodEvil.Evil => AlignmentResources.GoodEvil_Evil,
                _ => AlignmentResources.GoodEvil_Unknown
            };
}

public enum GoodEvil : sbyte
{
    Good = 1,
    Neutral = 0,
    Evil = -1
}

public enum LawfulChaotic : sbyte
{
    Lawful = 1,
    Neutral = 0,
    Chaotic = -1
}
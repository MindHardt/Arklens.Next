using System.Runtime.CompilerServices;
using Arklens.Next.Core;
using EnumerationGenerator;
using Resources.Next;
using Resources.Next.Generated;

namespace Arklens.Next.Entities;

[AlidDomain]
[GenerateEnumeration]
public partial record Deity : LocalizedAlidEntity<DeityResources>
{
    public Alignment Alignment { get; }

    private Deity(Alignment alignment, [CallerMemberName] string ownName = "") : base(ownName)
    {
        Alignment = alignment;
    }

    public static Deity Neras { get; } = new(Alignment.LawfulGood);
    public static Deity Sol { get; } = new(Alignment.NeutralGood);
    public static Deity Yunai { get; } = new(Alignment.ChaoticGood);
    public static Deity Avar { get; } = new(Alignment.LawfulNeutral);
    public static Deity Justar { get; } = new(Alignment.Neutral);
    public static Deity Mortess { get; } = new(Alignment.ChaoticNeutral);
    public static Deity Archivarius { get; } = new(Alignment.LawfulEvil);
    public static Deity Asterio { get; } = new(Alignment.NeutralEvil);
    public static Deity Sanguise { get; } = new(Alignment.ChaoticEvil);
}
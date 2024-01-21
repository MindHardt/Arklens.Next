using System.Globalization;
using System.Runtime.CompilerServices;
using Arklens.Next.Core;
using EnumerationGenerator;
using ResourcesGenerator;
using SourceGeneratedAlidSearchGenerator;

namespace Arklens.Next.Entities;

[AlidDomain]
[GenerateEnumeration]
[SearchInclude]
public partial record Deity : AlidEntity
{
    private readonly Func<CultureInfo?, string> _localizationFactory;

    public Alignment Alignment { get; }
    public override string GetLocalizedName(CultureInfo? cultureInfo = null) => _localizationFactory(cultureInfo);

    private Deity(Alignment alignment, [CallerMemberName] string ownName = "") : base(ownName)
    {
        Alignment = alignment;
        _localizationFactory = culture => DeityResources.Find(ownName, culture);
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
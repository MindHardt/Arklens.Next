using System.Globalization;
using System.Runtime.CompilerServices;
using Arklens.Next.Core;
using EnumerationGenerator;
using ResourcesGenerator;
using SourceGeneratedAlidSearchGenerator;

namespace Arklens.Next.Entities;

[AlidDomain("damage")]
[SearchInclude]
[GenerateEnumeration]
public partial record DamageType : AlidEntity
{
    private readonly LocalizationFactory _localizationFactory;

    public override string GetLocalizedName(CultureInfo? cultureInfo = null) => _localizationFactory(cultureInfo);

    /// <summary>
    /// Flags used to indicate some damage type properties.
    /// </summary>
    public DamageTypeFlags Flags { get; }
    /// <summary>
    /// Included child types.
    /// </summary>
    public IReadOnlyCollection<DamageType> IncludedTypes { get; }

    private DamageType(
        DamageTypeFlags flags = default,
        IReadOnlyCollection<DamageType>? includedTypes = null,
        [CallerMemberName] string ownName = "") : base(ownName)
    {
        IncludedTypes = includedTypes ?? [];
        Flags = flags | includedTypes?
            .Select(x => x.Flags)
            .Aggregate((r, l) => r | l)
            ?? default;
        _localizationFactory = DamageResources.Find(ownName);
    }
}

[Flags]
public enum DamageTypeFlags
{
    Physical = 1<<0,
    Magical = 1<<1,
    Elemental = 1<<2,
    Natural = 1<<3
}
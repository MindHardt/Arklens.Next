using System.Globalization;
using System.Runtime.CompilerServices;
using Arklens.Next.Core;
using EnumerationGenerator;
using ResourcesGenerator;
using SourceGeneratedAlidSearchGenerator;

namespace Arklens.Next.Entities.Traits;

[AlidDomain]
[SearchInclude]
[GenerateEnumeration]
public partial record Trait : AlidEntity
{
    private readonly Func<CultureInfo?, string> _localizationFactory;
    public override string GetLocalizedName(CultureInfo? cultureInfo = null) => _localizationFactory(cultureInfo);

    public Trait([CallerMemberName] string ownName = "") : base(ownName)
    {
        _localizationFactory = culture => TraitResources.Find(ownName, culture);
    }
}
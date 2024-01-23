using System.Runtime.CompilerServices;
using Arklens.Next.Core;
using EnumerationGenerator;
using ResourcesGenerator;

namespace Arklens.Next.Entities;

[AlidDomain]
[GenerateEnumeration]
public partial record Trait : AlidEntity
{
    public override string GetEmoji() => "⚒️";

    public Trait([CallerMemberName] string ownName = "") : base(ownName, TraitResources.FindString)
    { }
}
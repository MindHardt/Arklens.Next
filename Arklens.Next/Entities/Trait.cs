using System.Runtime.CompilerServices;
using Arklens.Next.Core;
using EnumerationGenerator;
using ResourcesGenerator;

namespace Arklens.Next.Entities.Traits;

[AlidDomain]
[GenerateEnumeration]
public partial record Trait : AlidEntity
{
    public Trait([CallerMemberName] string ownName = "") : base(ownName, TraitResources.FindString)
    { }
}
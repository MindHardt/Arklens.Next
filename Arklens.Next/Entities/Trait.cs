using System.Runtime.CompilerServices;
using Arklens.Next.Core;
using EnumerationGenerator;
using Resources.Next;
using Resources.Next.Generated;

namespace Arklens.Next.Entities;

[AlidDomain]
[GenerateEnumeration]
public partial record Trait : LocalizedAlidEntity<TraitResources>
{
    public override string Emoji => "⚒️";

    public Trait([CallerMemberName] string ownName = "") : base(ownName)
    { }
}
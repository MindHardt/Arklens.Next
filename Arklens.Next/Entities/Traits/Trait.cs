using System.Runtime.CompilerServices;
using Arklens.Next.Core;
using EnumerationGenerator;
using SourceGeneratedAlidSearchGenerator;

namespace Arklens.Next.Entities.Traits;

[AlidDomain]
[SearchInclude]
[GenerateEnumeration]
public partial record Trait : AlidEntity
{
    public Trait([CallerMemberName] string ownName = "") : base(ownName)
    {
    }
}
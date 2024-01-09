using System.Runtime.CompilerServices;
using Arklens.Next.Core;
using EnumerationGenerator;
using SourceGeneratedAlidSearchGenerator;

namespace Arklens.Next.Entities.Traits;

[AlidDomain]
[SearchInclude]
[GenerateEnumeration]
public partial record Trait : IAlidEntity
{
    public AlidName OwnName { get; }

    private Trait([CallerMemberName] string ownName = "")
    {
        OwnName = AlidName.Create(ownName);
    }
}
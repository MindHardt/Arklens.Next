using Arklens.Next.Core;
using SourceGeneratedAlidSearchGenerator;

namespace Arklens.Next.Search;

public interface IAlidSearch
{
    /// <summary>
    /// The default instance of <see cref="IAlidSearch"/>.
    /// </summary>
    public static IAlidSearch Default => SourceGeneratedAlidSearch.Instance;

    public AlidEntity? Get(Alid alid);
    public IReadOnlyCollection<AlidEntity> IncludedEntities { get; }
}
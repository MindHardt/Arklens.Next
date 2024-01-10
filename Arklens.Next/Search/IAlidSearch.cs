using Arklens.Next.Core;

namespace Arklens.Next.Search;

public interface IAlidSearch
{
    public AlidEntity? Get(Alid alid);
    public IReadOnlyCollection<AlidEntity> IncludedEntities { get; }
}
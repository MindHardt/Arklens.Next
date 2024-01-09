using Arklens.Next.Core;

namespace Arklens.Next.Search;

public interface IAlidSearch
{
    public IAlidEntity? Get(Alid alid);
}
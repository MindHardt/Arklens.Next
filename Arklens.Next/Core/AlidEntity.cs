using System.Runtime.CompilerServices;

namespace Arklens.Next.Core;

public abstract record AlidEntity
{
    /// <summary>
    /// This instance's <see cref="AlidName"/>.
    /// </summary>
    public AlidName OwnName { get; }

    protected AlidEntity(string ownName)
    {
        OwnName = AlidName.Create(ownName);
    }

    /// <summary>
    /// The full <see cref="Alid"/> of this <see cref="AlidEntity"/>.
    /// </summary>
    public virtual Alid Alid => new(GetType().GetDomains(), OwnName);
}
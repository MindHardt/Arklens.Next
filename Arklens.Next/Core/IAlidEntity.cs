namespace Arklens.Next.Core;

public interface IAlidEntity
{
    /// <summary>
    /// This instance's <see cref="AlidName"/>.
    /// </summary>
    public AlidName OwnName { get; }

    /// <summary>
    /// The full <see cref="Alid"/> of this <see cref="IAlidEntity"/>.
    /// </summary>
    public virtual Alid Alid => new(GetType().GetDomains(), OwnName);
}
using System.Globalization;

namespace Arklens.Next.Core;

public abstract record AlidEntity
{
    /// <summary>
    /// This instance's <see cref="AlidName"/>.
    /// </summary>
    public AlidName OwnName { get; }

    /// <summary>
    /// Gets a localized name of this <see cref="AlidEntity"/>.
    /// </summary>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    public abstract string GetLocalizedName(CultureInfo? cultureInfo = null);

    protected AlidEntity(string ownName)
    {
        OwnName = AlidName.Create(ownName);
    }

    /// <summary>
    /// The full <see cref="Alid"/> of this <see cref="AlidEntity"/>.
    /// </summary>
    public virtual Alid Alid => new(GetType().GetDomains(), OwnName);
}
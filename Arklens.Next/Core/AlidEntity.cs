using System.Globalization;

namespace Arklens.Next.Core;

public abstract partial record AlidEntity
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

    /// <summary>
    /// Gets an <see cref="AlidEntity"/> whose <see cref="Alid"/>
    /// has <see cref="Alid.Text"/> equal to <paramref name="alid"/>
    /// or <see langword="null"/> if none is found.
    /// </summary>
    /// <param name="alid"></param>
    /// <returns></returns>
    public static partial AlidEntity? Get(string alid);
    /// <inheritdoc cref="Get(string)"/>
    public static AlidEntity? Get(Alid alid) => Get(alid.Text);

    /// <summary>
    /// Identical to <see cref="Get(string)"/>, but throws
    /// <see cref="KeyNotFoundException"/> if
    /// entity is not found.
    /// </summary>
    /// <param name="alid"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public static AlidEntity GetRequired(string alid) => Get(alid) ?? throw new KeyNotFoundException();
    /// <inheritdoc cref="GetRequired(string)"/>
    public static AlidEntity GetRequired(Alid alid) => GetRequired(alid.Text);
}
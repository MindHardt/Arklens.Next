using System.Globalization;
using Resources.Next;

namespace Arklens.Next.Core;

public abstract partial record AlidEntity
{
    /// <summary>
    /// This instance's <see cref="AlidName"/>.
    /// </summary>
    public AlidName OwnName { get; }
    
    /// <summary>
    /// Gets the emoji of this <see cref="AlidEntity"/>.
    /// </summary>
    public abstract string Emoji { get; }

    /// <summary>
    /// Gets localized name of this <see cref="AlidEntity"/>.
    /// </summary>
    /// <param name="culture"></param>
    /// <returns></returns>
    public abstract string GetName(CultureInfo? culture = null);

    
    // ReSharper disable once VirtualMemberCallInConstructor
    protected AlidEntity(string ownName)
    {
        OwnName = AlidName.Create(ownName);
    }

    /// <summary>
    /// The full <see cref="Alid"/> of this <see cref="AlidEntity"/>.
    /// </summary>
    public virtual OwnAlid Alid => new(GetType().GetDomains(), OwnName);

    #region Lookup methods

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
    /// Gets an <see cref="AlidEntity"/> whose <see cref="Alid"/>
    /// has <see cref="Alid.Text"/> equal to <paramref name="alid"/>
    /// or <see langword="null"/> if none is found or found entity
    /// is not <typeparamref name="TEntity"/>.
    /// </summary>
    /// <param name="alid"></param>
    /// <returns></returns>
    public static TEntity? Get<TEntity>(string alid) where TEntity : AlidEntity => Get(alid) as TEntity;
    /// <inheritdoc cref="Get{TEntity}(string)"/>
    public static TEntity? Get<TEntity>(Alid alid) where TEntity : AlidEntity => Get<TEntity>(alid.Text);

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

    /// <summary>
    /// Identical to <see cref="Get(string)"/>, but throws
    /// <see cref="KeyNotFoundException"/> if
    /// entity is not found or <see cref="InvalidCastException"/>
    /// if found entity is not <typeparamref name="TEntity"/>.
    /// </summary>
    /// <param name="alid"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public static TEntity GetRequired<TEntity>(string alid) where TEntity : AlidEntity => (TEntity)GetRequired(alid);
    /// <inheritdoc cref="GetRequired{TEntity}(string)"/>
    public static TEntity GetRequired<TEntity>(Alid alid) where TEntity : AlidEntity => GetRequired<TEntity>(alid.Text);

    #endregion
}
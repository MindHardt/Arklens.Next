using System.Globalization;

namespace Arklens.Next.Core;

public abstract partial record AlidEntity
{
    private readonly LocalizationFactory _localizationFactory;

    /// <summary>
    /// This instance's <see cref="AlidName"/>.
    /// </summary>
    public AlidName OwnName { get; }

    /// <summary>
    /// Gets a localized name of this <see cref="AlidEntity"/>.
    /// </summary>
    /// <param name="culture"></param>
    /// <returns></returns>
    protected virtual string GetLocalizedName(string culture) => _localizationFactory(culture);

    /// <inheritdoc cref="GetLocalizedName(string)"/>
    public string GetLocalizedName(CultureInfo? cultureInfo = null)
        => GetLocalizedName((cultureInfo ?? Thread.CurrentThread.CurrentCulture).TwoLetterISOLanguageName);

    public const string EmojiCulture = "Emoji";

    /// <summary>
    /// Gets Emoji of this <see cref="AlidEntity"/>
    /// </summary>
    /// <returns></returns>
    public virtual string GetEmoji()
        => GetLocalizedName(EmojiCulture);

    protected AlidEntity(string ownName, Func<string, LocalizationFactory> resourceProvider)
    {
        OwnName = AlidName.Create(ownName);
        _localizationFactory = resourceProvider(ownName);
    }

    /// <summary>
    /// The full <see cref="Alid"/> of this <see cref="AlidEntity"/>.
    /// </summary>
    public virtual Alid Alid => new(GetType().GetDomains(), OwnName);

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
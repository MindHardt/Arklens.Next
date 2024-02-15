#pragma warning disable CS0436
using System.Globalization;
using Resources.Next;

namespace Arklens.Next.Core;

/// <summary>
/// Represents an <see cref="AlidEntity"/> that is localized with <see cref="LocalizedResource"/>s.
/// </summary>
/// <typeparam name="TResource">A resources class with localizations of this type.</typeparam>
public abstract record LocalizedAlidEntity<TResource> : AlidEntity
    where TResource: IResourceProvider
{
    private readonly LocalizedResource _resource;
    
    /// <summary>
    /// A culture used for <see cref="LocalizedResource"/>s
    /// that store <see cref="Emoji"/> of this <see cref="AlidEntity"/>.
    /// </summary>
    public const string EmojiCulture = "Emoji";

    /// <summary>
    /// A default emoji used if no emoji is defined for this <see cref="AlidEntity"/>.
    /// </summary>
    public const string DefaultEmoji = "❔";

    public override string Emoji => _resource.GetOrNull(EmojiCulture) ?? DefaultEmoji;
    public override string GetName(CultureInfo? culture = null) => _resource.GetOrDefault(culture);

    public LocalizedAlidEntity(string ownName) : base(ownName)
    {
        _resource = TResource.Find(ownName) ?? throw new KeyNotFoundException(
            $"A resource with key {ownName} not found for type {GetType().Name}");
    }
}
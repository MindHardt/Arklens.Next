﻿using System.Globalization;

namespace Arklens.Next.Core;

public static class AlidExtensions
{
    /// <summary>
    /// Gets a display string for this <see cref="AlidEntity"/> with emoji.
    /// </summary>
    /// <example>🧝 Elf</example>
    public static string ToDisplayString(this AlidEntity entity, CultureInfo? cultureInfo = null)
        => $"{entity.Emoji} {entity.GetName(cultureInfo)}";

    /// <summary>
    /// Packs this <see cref="IEnumerable{T}"/> into a <see cref="AlidNameCollection"/>.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static AlidNameCollection ToAlidNameCollection(this IEnumerable<AlidName> source)
        => new(source);

    /// <summary>
    /// Creates a new <see cref="AlidNameCollection"/> from this <see cref="IEnumerable{T}"/>.
    /// This method uses <see cref="AlidNameCollection.Create(IEnumerable{string})"/>.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static AlidNameCollection CreateAlidNameCollection(this IEnumerable<string> source)
        => AlidNameCollection.Create(source);

    /// <summary>
    /// Creates a new <see cref="AlidName"/> from this <see cref="string"/>.
    /// This method uses <see cref="AlidName.Create(string)"/>.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static AlidName CreateAlidName(this string value)
        => AlidName.Create(value);
}
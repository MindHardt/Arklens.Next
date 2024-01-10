using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Arklens.Next.Core;

/// <summary>
/// A part of <see cref="Alid"/>. This can represent a domain, own name or modifier name.
/// </summary>
/// <param name="Value"></param>
public readonly partial record struct AlidName(string Value)
{
    /// <summary>
    /// A partial (without ^$) regex string used for validation of <see cref="AlidName"/>s.
    /// </summary>
    [StringSyntax(StringSyntaxAttribute.Regex)]
    public const string AlidNameRegexPartial = "[a-z0-9_]+";

    /// <summary>
    /// A <see cref="Regex"/> used to validate <see cref="AlidName"/>s.
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex($"^{AlidNameRegexPartial}$")]
    public static partial Regex ValidationRegex();

    /// <summary>
    /// The inner <see cref="string"/> value of this <see cref="AlidName"/>.
    /// </summary>
    public string Value { get; } = ValidationRegex().IsMatch(Value)
        ? Value
        : throw new ArgumentException($"Input string is not a valid {nameof(AlidName)}", nameof(Value));

    /// <summary>
    /// Creates a new <see cref="AlidName"/> from <paramref name="value"/>.
    /// This method will transform <paramref name="value"/> from PascalCase
    /// or camelCase to snake_case using
    /// <see cref="AlidUtilities.ToSnakeCase(string)"/>.
    /// </summary>
    /// <param name="value">The created string.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">
    /// If <paramref name="value"/> contains unprocessable symbols.
    /// </exception>
    public static AlidName Create(string value)
        => new(value.ToSnakeCase());

    /// <summary>
    /// For <see cref="AlidName"/>s, returns <see cref="Value"/>.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
        => Value;

    public static implicit operator string(AlidName alidName)
        => alidName.Value;

    public static implicit operator AlidName(string value)
        => new(value);
}
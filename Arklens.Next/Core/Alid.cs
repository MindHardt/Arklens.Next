using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Arklens.Next.Core;

/// <summary>
/// <para>
/// Alid (acronym of Arklens ID) is a way to uniform ids for everything in arklens code.
/// <see cref="Alid"/> is inspired by and is based on minecraft text ids.
/// </para>
/// <para>
/// Examples of valid <see cref="Alid"/>s:
/// <code>
/// spell:wizard:fireball
/// trait:expert:swimming
/// weapon:rapier+well_made+flexible
/// </code>
/// </para>
/// </summary>
public abstract partial record Alid(
    AlidNameCollection Domains)
    : IParsable<Alid>
{
    public const int MaxLength = 128;

    [StringSyntax(StringSyntaxAttribute.Regex)]
    public const string ValidationRegexString =
        @$"^(?<Domains>({AlidName.AlidNameRegexPartial}\:)+)(?<Name>\*|#?{AlidName.AlidNameRegexPartial})(?<Modifiers>(\+{AlidName.AlidNameRegexPartial})*)$";

    [GeneratedRegex(ValidationRegexString)]
    public static partial Regex ValidationRegex();

    public static Alid Undefined { get; } =
        new OwnAlid(AlidNameCollection.Create(nameof(Alid)), AlidName.Create("Undefined"));

    public abstract AlidType Type { get; }
    public abstract string Text { get; }

    public AlidNameCollection Domains { get; } = Domains.Count > 0
        ? Domains
        : throw new ArgumentException("Alids are required to have at least one domain.");

    /// <summary>
    /// For <see cref="Alid"/>s, returns <see cref="Text"/>.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
        => Text;

    public virtual bool Equals(Alid? other)
        => other?.Text == Text;

    public override int GetHashCode()
        => Text.GetHashCode();

    public static Alid Parse(string s, IFormatProvider? provider = null)
    {
        ArgumentNullException.ThrowIfNull(s);
        if (s.Length > MaxLength)
        {
            throw new ArgumentException(
                $"Value is too long for an alid. Maximum allowed length is {MaxLength}.",
                nameof(s));
        }

        if (ValidationRegex().Match(s) is not { Success: true } match)
        {
            throw new ArgumentException(
                $"Value failed alid validation. Alids must match with regex {ValidationRegexString}",
                nameof(s));
        }

        return BuildFromMatch(match);
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Alid result)
    {
        result = Undefined;
        if (s is not { Length: <= MaxLength } || ValidationRegex().Match(s) is not { Success: true } match)
        {
            return false;
        }

        result = BuildFromMatch(match);
        return true;
    }

    private static Alid BuildFromMatch(Match match)
    {
        var domains = AlidNameCollection.FromDomainsString(match.Groups["Domains"].Value);

        var name = match.Groups["Name"].Value;
        if (name == "*")
        {
            return new DomainSelectorAlid(domains);
        }
        if (name.StartsWith('#'))
        {
            return new GroupAlid(domains, AlidName.Create(name));
        }

        var modifiers = AlidNameCollection.FromModifiersString(match.Groups["Modifiers"].Value);

        return new OwnAlid(domains, AlidName.Create(name), modifiers);
    }
}
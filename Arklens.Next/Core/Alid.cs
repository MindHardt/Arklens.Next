using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Arklens.Next.Core;

/// <summary>
/// <para>
/// Alid (acronym of Arklens ID) is a way to uniform ids for everything in arklens code.
/// <see cref="Alid"/> is inspired by and is based on minecraft text ids.
/// <see cref="Alid"/>s are written in snake-case and consist of following parts:
/// </para>
/// <para>
/// ○ <see cref="Domains"/>, which describe the type of an entity. Entity must have at least one domain.
/// </para>
/// <para>
/// ○ <see cref="Name"/>, which must be unique per domain.
/// </para>
/// <para>
/// ○ <see cref="Modifiers"/>, which are responsible for variations within the type. They are optional in most cases.
/// </para>
/// <para>
/// Examples of valid <see cref="Alid"/>s:
/// </para>
/// <code>
/// spell:wizard:fireball
/// trait:expert+swimming
/// weapon:rapier+well_made+flexible
/// </code>
/// </summary>
public partial record Alid(
    AlidNameCollection Domains,
    AlidName Name,
    AlidNameCollection? Modifiers = null,
    bool IsGroup = false)
    : IParsable<Alid>
{
    public const int MaxLength = 128;

    [StringSyntax(StringSyntaxAttribute.Regex)]
    public const string ValidationRegexString =
        @$"^(?<Domains>({AlidName.AlidNameRegexPartial}\:)+)(?<Name>#?{AlidName.AlidNameRegexPartial})(?<Modifiers>(\+{AlidName.AlidNameRegexPartial})*)$";

    [GeneratedRegex(ValidationRegexString)]
    public static partial Regex ValidationRegex();

    public AlidNameCollection Domains { get; } = Domains.Count > 0
        ? Domains
        : throw new ArgumentException("Alids are required to have at least one domain.");
    public AlidNameCollection Modifiers { get; } = Modifiers ?? AlidNameCollection.Empty;

    public string Text { get; } =
        Domains.ToDomainsString() +
        (IsGroup ? "#" : "") +
        Name +
        Modifiers?.ToModifiersString();

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

    public static Alid Undefined { get; } = Create(["alid"], "undefined");

    public static Alid Create(
        IEnumerable<string> domains,
        string name,
        IEnumerable<string>? modifiers = null,
        bool isGroup = false)
        => new(domains.CreateAlidNameCollection(), name.CreateAlidName(), modifiers?.CreateAlidNameCollection(), isGroup);

    public static Alid Parse(string s, IFormatProvider? provider = null)
    {
        if (s.Length > MaxLength || ValidationRegex().Match(s) is not { Success: true } match)
        {
            throw new FormatException($"Input string is not a valid {nameof(Alid)}");
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
        var domains = match.Groups["Domains"].Value
            .Split(':', StringSplitOptions.RemoveEmptyEntries)
            .CreateAlidNameCollection();
        var modifiers = match.Groups["Modifiers"].Value
            .Split('+', StringSplitOptions.RemoveEmptyEntries)
            .CreateAlidNameCollection();
        var nameGroup = match.Groups["Name"].Value;
        var isGroup = nameGroup.StartsWith('#');
        var name = nameGroup
            .TrimStart('#')
            .CreateAlidName();

        return new Alid(domains, name, modifiers, isGroup);
    }
}
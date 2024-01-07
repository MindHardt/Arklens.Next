using System.Collections;

namespace Arklens.Next.Core;

public record AlidNameCollection : IReadOnlyList<AlidName>
{
    public IReadOnlyList<AlidName> Values { get; } = [];

    public IEnumerator<AlidName> GetEnumerator()
        => Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => Values.GetEnumerator();

    public int Count
        => Values.Count;

    public AlidName this[int index]
        => Values[index];

    public AlidNameCollection(IEnumerable<AlidName> names)
    {
        Values = [..names];
    }

    public AlidNameCollection(ReadOnlySpan<AlidName> names)
    {
        Values = [..names];
    }

    /// <summary>
    /// Formats this <see cref="AlidNameCollection"/> as a domains list.
    /// </summary>
    /// <returns></returns>
    public string ToDomainsString()
        => string.Concat(Values.Select(x => $"{x}:"));

    /// <summary>
    /// Formats this <see cref="AlidNameCollection"/> as a modifiers list.
    /// </summary>
    /// <returns></returns>
    public string ToModifiersString()
        => string.Concat(Values.Select(x => $"+{x}"));

    /// <summary>
    /// An empty instance of <see cref="AlidNameCollection"/>.
    /// </summary>
    public static AlidNameCollection Empty { get; } = new(Enumerable.Empty<AlidName>());

    /// <summary>
    /// Creates a new <see cref="AlidNameCollection"/> from
    /// <paramref name="values"/>, transforming them to <see cref="AlidName"/>s
    /// using <see cref="AlidName.Create(string)"/>.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static AlidNameCollection Create(IEnumerable<string> values)
        => new(values.Select(AlidName.Create));

    /// <inheritdoc cref="Create(System.Collections.Generic.IEnumerable{string})"/>
    public static AlidNameCollection Create(params string[] values)
        => Create(values.AsEnumerable());

    /// <inheritdoc cref="Create(System.Collections.Generic.IEnumerable{string})"/>
    public static AlidNameCollection Create(ReadOnlySpan<string> values)
        => Create(values.ToArray());

    public virtual bool Equals(AlidNameCollection? other)
        => other?.Values.SequenceEqual(Values) is true;

    public override int GetHashCode()
        => Values.GetHashCode();
}
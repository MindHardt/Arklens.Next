namespace Arklens.Next.Core;

/// <summary>
/// Indicates that decorated class represents an alid domain.
/// </summary>
/// <param name="explicitName"></param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class AlidDomainAttribute(string? explicitName = null) : Attribute
{
    // ReSharper disable once SuggestBaseTypeForParameterInConstructor
    public AlidDomainAttribute(Type decoratedType) : this(decoratedType.Name)
    { }

    /// <summary>
    /// The explicitly set <see cref="AlidName"/> of this domain.
    /// </summary>
    public AlidName? ExplicitName => explicitName?.CreateAlidName();
}
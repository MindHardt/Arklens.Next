namespace Arklens.Next.Core;

public enum AlidType : byte
{
    /// <summary>
    /// Own alid name of an exact entity.
    /// </summary>
    /// <example>weapon:longsword</example>
    Own,
    /// <summary>
    /// A named group of alids.
    /// </summary>
    /// <example>weapon:#druid</example>
    Group,
    /// <summary>
    /// A capture-all selector of a domain.
    /// </summary>
    /// <example>spell:wizard:*</example>
    DomainSelector
}
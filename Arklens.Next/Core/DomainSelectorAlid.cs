namespace Arklens.Next.Core;

public record DomainSelectorAlid(
    AlidNameCollection Domains)
    : Alid(Domains)
{
    public override AlidType Type => AlidType.DomainSelector;

    public override string Text =>
        $"{Domains.ToDomainsString()}*";
}
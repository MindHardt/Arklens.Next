namespace Arklens.Next.Core;

public class DomainSelectorAlid(
    AlidNameCollection Domains)
    : Alid(Domains)
{
    public override AlidType Type => AlidType.DomainSelector;

    public override string Text =>
        $"{Domains.ToDomainsString()}*";
}
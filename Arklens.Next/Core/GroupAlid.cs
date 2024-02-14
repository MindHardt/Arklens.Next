namespace Arklens.Next.Core;

public record GroupAlid(
    AlidNameCollection Domains,
    AlidName GroupName)
    : Alid(Domains)
{
    public override AlidType Type => AlidType.Group;
    public override string Text =>
        $"{Domains.ToDomainsString()}#{GroupName}";
}
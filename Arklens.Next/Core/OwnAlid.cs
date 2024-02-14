namespace Arklens.Next.Core;

public record OwnAlid(
    AlidNameCollection Domains,
    AlidName OwnName,
    AlidNameCollection? Modifiers = null) : Alid(Domains)
{
    public AlidNameCollection Modifiers { get; } = Modifiers ?? AlidNameCollection.Empty;

    public override AlidType Type => AlidType.Own;
    public override string Text =>
        $"{Domains.ToDomainsString()}{OwnName}{Modifiers.ToModifiersString()}";

    public static OwnAlid Create(IEnumerable<string> domains, string ownName, IEnumerable<string>? modifiers = null)
        => new(AlidNameCollection.Create(domains), AlidName.Create(ownName), AlidNameCollection.Create(modifiers));
}
namespace Arklens.Next.Entities;

public partial record DamageType
{
    public static DamageType Venom { get; } = new(DamageTypeFlags.Natural);
}
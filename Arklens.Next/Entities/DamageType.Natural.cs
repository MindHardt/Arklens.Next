namespace Arklens.Next.Entities;

public partial record DamageType
{
    public static DamageType Poison { get; } = new(DamageTypeFlags.Natural);
}
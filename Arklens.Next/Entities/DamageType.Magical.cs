namespace Arklens.Next.Entities;

public partial record DamageType
{
    public static DamageType Curse { get; } = new(DamageTypeFlags.Magical);
}
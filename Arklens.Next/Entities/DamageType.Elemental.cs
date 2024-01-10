namespace Arklens.Next.Entities;

public partial record DamageType
{
    public static DamageType Fire { get; } = new(DamageTypeFlags.Elemental);
    public static DamageType Frost { get; } = new(DamageTypeFlags.Elemental);
    public static DamageType Acid { get; } = new(DamageTypeFlags.Elemental);
    public static DamageType Lightning { get; } = new(DamageTypeFlags.Elemental);
    public static DamageType Sound { get; } = new(DamageTypeFlags.Elemental);
}
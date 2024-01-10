namespace Arklens.Next.Entities;

public partial record DamageType
{
    public static DamageType Piercing { get; } = new(DamageTypeFlags.Physical);
    public static DamageType Cutting { get; } = new(DamageTypeFlags.Physical);
    public static DamageType Chopping { get; } = new(DamageTypeFlags.Physical);
    public static DamageType Crushing { get; } = new(DamageTypeFlags.Physical);

    public static DamageType Bite { get; } = new(includedTypes: [Piercing, Cutting, Chopping, Crushing]);
    public static DamageType Claws { get; } = new(includedTypes: [Piercing, Cutting, Chopping]);
}
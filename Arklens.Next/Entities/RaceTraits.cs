namespace Arklens.Next.Entities;

public readonly record struct RaceTraits(Trait One, Trait Two)
{
    public Trait[] ToArray() => [One, Two];

    public static implicit operator RaceTraits((Trait one, Trait two) tuple)
        => new(tuple.one, tuple.two);
}
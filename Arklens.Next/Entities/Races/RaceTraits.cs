using Arklens.Next.Entities.Traits;

namespace Arklens.Next.Entities.Races;

public readonly record struct RaceTraits(Trait One, Trait Two)
{
    public Trait[] ToArray() => [One, Two];

    public static implicit operator RaceTraits((Trait one, Trait two) tuple)
        => new(tuple.one, tuple.two);
}
using System.Collections;
using System.Collections.Frozen;
using System.Reflection;
using Arklens.Next.Core;
using EnumerationGenerator;

namespace Arklens.Next.Search;

/// <summary>
/// An implementation of <see cref="IAlidSearch"/> that uses reflection
/// to scan assemblies at the runtime.
/// </summary>
public class ReflectiveAlidSearch : IAlidSearch
{
    private readonly FrozenDictionary<Alid, AlidEntity> _innerDictionary;

    public IReadOnlyCollection<Assembly> IncludedAssemblies { get; }

    public ReflectiveAlidSearch(params Assembly[] assemblies)
    {
        IncludedAssemblies = assemblies
            .Append(typeof(ReflectiveAlidSearch).Assembly)
            .Distinct()
            .ToArray();

        var enumTypes = IncludedAssemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => x.GetInterfaces().Any(@interface =>
                @interface.IsGenericType &&
                @interface.GetGenericTypeDefinition() == typeof(IEnumeration<>)));

        var tempDictionary = new Dictionary<Alid, AlidEntity>();
        foreach (var enumType in enumTypes)
        {
            var allValuesProperty = enumType.GetProperty(nameof(IEnumeration<object>.AllValues))!;
            var allValues = allValuesProperty.GetValue(null) as IEnumerable;

            foreach (var value in allValues!.Cast<AlidEntity>())
            {
                tempDictionary.TryAdd(value.Alid, value);
            }
        }

        _innerDictionary = tempDictionary.ToFrozenDictionary(
            x => x.Key,
            x => x.Value);
    }

    public AlidEntity? Get(Alid alid)
        => _innerDictionary.GetValueOrDefault(alid);

    public IReadOnlyCollection<AlidEntity> IncludedEntities => _innerDictionary.Values;
}
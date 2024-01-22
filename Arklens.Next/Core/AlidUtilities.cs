using System.Reflection;
using System.Text;

namespace Arklens.Next.Core;

public static class AlidUtilities
{
    /// <summary>
    /// Converts <paramref name="value"/> from PascalCase or camelCase to
    /// snake_case.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>
    /// This only works with characters that are recognised by <see cref="char.IsUpper(char)"/>.
    /// </remarks>
    public static string ToSnakeCase(this string value)
    {
        var newLength = value.Length + value.Where(char.IsUpper).Count();

        if (newLength == value.Length)
        {
            return value;
        }

        var sb = new StringBuilder(newLength);
        sb.Append(char.ToLower(value[0]));

        foreach (var symbol in value[1..])
        {
            ReadOnlySpan<char> appendedValue = char.IsUpper(symbol)
                ? ['_', char.ToLower(symbol)]
                : [symbol];

            sb.Append(appendedValue);
        }

        return sb.ToString();
    }

    private static readonly Dictionary<Type, AlidNameCollection> DomainsCache = new();
    /// <summary>
    /// Gets a <see cref="AlidNameCollection"/> that contains names for domains of this type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="cache">Whether the result should be cached for reuse.</param>
    /// <returns></returns>
    /// <remarks>This method relies on reflection so the results can be cached for better performance.</remarks>
    public static AlidNameCollection GetDomains(this Type type, bool cache = true)
    {
        if (DomainsCache.TryGetValue(type, out var cachedDomains))
        {
            return cachedDomains;
        }

        var domains = new List<AlidName>();
        var currentType = type;
        while (currentType is not null)
        {
            if (currentType.GetCustomAttribute<AlidDomainAttribute>() is { } domainAttribute)
            {
                var domainName = domainAttribute.ExplicitName
                    ?? AlidName.Create(currentType.Name);
                domains.Add(domainName);
            }

            currentType = currentType.BaseType;
        }

        var result = new AlidNameCollection(domains);
        if (cache)
        {
            DomainsCache.Add(type, result);
        }

        return result;
    }
}
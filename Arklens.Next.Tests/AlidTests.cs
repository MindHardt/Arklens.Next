using System.Globalization;
using System.Text.Json;
using Arklens.Next.Core;
using Arklens.Next.Entities;
using Arklens.Next.Extra;
using Xunit.Abstractions;
using Trait = Arklens.Next.Entities.Trait;

namespace Arklens.Next.Tests;

public class AlidTests(ITestOutputHelper output)
{
    private static readonly CultureInfo[] IncludedCultures =
    [
        CultureInfo.GetCultureInfo("en"), CultureInfo.GetCultureInfo("ru")
    ];

    [Theory]
    [InlineData("weapon")]
    [InlineData("spell")]
    [InlineData("fireball")]
    [InlineData("well_made")]
    [InlineData("fury_1")]
    public void TestAlidValidationSuccessful(string validAlidName)
    {
        _ = new AlidName(validAlidName);
    }

    [Theory]
    [InlineData("Weapon")]
    [InlineData("GreaterFire")]
    [InlineData("camelCaseName")]
    public void TestAlidCreation(string wrongCaseAlidName)
    {
        _ = AlidName.Create(wrongCaseAlidName);
    }

    [Theory]
    [InlineData("weapon:longsword")]
    [InlineData("spell:wizard:fireball+enhanced")]
    [InlineData("weapon:rapier+well_made+flexible")]
    [InlineData("alid:undefined")]
    public void TestAlidParseSuccessful(string alidText)
    {
        Assert.True(Alid.TryParse(alidText, null, out var alid));
        Assert.Equal(alidText, alid.Text);
    }

    [Theory]
    [InlineData("illegal")]
    [InlineData("alid:f*rbiddenCh^rs")]
    [InlineData("alid:which_is_too_long_to_be_validated_because_maximum_length_of_alid_text_is_128_characters_but_this_string_is_131_characters_long")]
    public void TestAlidParseFail(string alidText)
    {
        Assert.False(Alid.TryParse(alidText, null, out var undefined));
        Assert.Same(undefined, Alid.Undefined);
        Assert.Throws<FormatException>(() => Alid.Parse(alidText));
    }

    [Theory]
    [InlineData("alignment:neutral", typeof(Alignment))]
    [InlineData("deity:mortess", typeof(Deity))]
    [InlineData("race:human", typeof(Race))]
    [InlineData("trait:handyman", typeof(Trait))]
    public void TestAlidSearches(string alid, Type expectedType)
    {
        Assert.IsType(expectedType, AlidEntity.Get(alid));
    }

    [Fact]
    public void TestLocalizedNames()
    {
        var includedEntities = AlidEntity.AllValues
            .OrderBy(x => x.Alid.Text)
            .Select((x, i) => (x, i));
        foreach (var entity in includedEntities)
        {
            output.WriteLine($"{entity.i + 1}. {entity.x.Alid} {entity.x.ToDisplayString()}");

            var localizations = IncludedCultures
                .Select(culture => entity.x.GetName(culture))
                .ToArray();

            Assert.NotEqual(localizations[0], localizations[1]);
        }
    }

    [Theory]
    [InlineData("\"deity:asterio\"", typeof(Deity))]
    [InlineData("\"damage:cutting\"", typeof(DamageType))]
    [InlineData("\"race:elf\"", typeof(Race))]
    public void TestJson(string json, Type expectedType)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            Converters = { new AlidEntityJsonConverterFactory() }
        };

        var entity = JsonSerializer.Deserialize(json, expectedType, jsonOptions);
        Assert.IsType(expectedType, entity);

        var serialized = JsonSerializer.Serialize(entity, jsonOptions);
        Assert.Equal(json, serialized);
    }
}
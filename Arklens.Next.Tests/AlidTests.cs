using Arklens.Next.Core;
using Arklens.Next.Entities;
using Arklens.Next.Entities.Races;
using Arklens.Next.Entities.Traits;
using Arklens.Next.Search;
using SourceGeneratedAlidSearchGenerator;

namespace Arklens.Next.Tests;

public class AlidTests
{
    private static readonly IAlidSearch[] AlidSearches =
    [
        new ReflectiveAlidSearch(), SourceGeneratedAlidSearch.Instance
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
        foreach (var search in AlidSearches)
        {
            Assert.IsType(expectedType, search.Get(Alid.Parse(alid)));
        }
    }
}
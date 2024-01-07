using Arklens.Next.Core;
using Arklens.Next.Entities.Alignments;
using Xunit.Abstractions;

namespace Arklens.Next.Tests;

public class AlidTests(ITestOutputHelper testOutputHelper)
{
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
    [InlineData("pascalCaseName")]
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

    [Fact]
    public void TestAlignmentsAlids()
    {
        foreach (var alignment in Alignment.AllValues)
        {
            testOutputHelper.WriteLine(alignment.GetAlid().Text);
        }
    }
}
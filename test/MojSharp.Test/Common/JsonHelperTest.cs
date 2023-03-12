using System.Text.Json;
using MojSharp.Common;
using Xunit;

namespace MojSharp.Test.Common;

public class JsonHelperTest
{
    [Theory]
    [InlineData(@"{""foo"":""bar""}", "foo", "bar")]
    [InlineData(@"{""  foo  "":""  bar  ""}", "  foo  ", "  bar  ")]
    public void GetKeyString_Returns_CorrectString(string json, string key, string expected)
    {
        // arrange
        using var doc = JsonDocument.Parse(json);

        // act
        var str = doc.RootElement.GetKeyString(key);

        // assert
        Assert.Equal(expected, str);
    }

    [Theory]
    [InlineData(@"{}", "foo")]
    [InlineData(@"{}", null)]
    [InlineData(@"{}", "")]
    [InlineData(@"{}", "   ")]
    [InlineData(@"{""foo"":""""}", "foo")]
    [InlineData(@"{""foo"":""   ""}", "foo")]
    [InlineData(@"{""foo"":123}", "foo")]
    public void GetKeyString_Throws_OnInvalidArgs(string json, string key)
    {
        // arrange
        using var doc = JsonDocument.Parse(json);

        // act, assert
        Assert.Throws<ArgumentException>(() => doc.RootElement.GetKeyString(key));
    }
}

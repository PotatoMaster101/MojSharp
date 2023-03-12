using System.Text.Json;
using MojSharp.Profile;
using Xunit;

namespace MojSharp.Test.Profile;

/// <summary>
/// Unit tests for <see cref="ProfileProperty"/>.
/// </summary>
public class ProfilePropertyTest
{
    [Theory]
    [InlineData("foo", "bar", null)]
    [InlineData("bar", "foo", "")]
    [InlineData("bar", "baz", "sig")]
    public void StringConstructor_Sets_Members(string name, string value, string sig)
    {
        // act
        var property = new ProfileProperty(name, value, sig);

        // assert
        Assert.Equal(name, property.Name);
        Assert.Equal(value, property.Value);
        Assert.Equal(sig, property.Signature);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "foo")]
    [InlineData("foo", null)]
    [InlineData("", "")]
    [InlineData("foo", "")]
    [InlineData("", "foo")]
    [InlineData("   ", "   ")]
    [InlineData("foo", "   ")]
    [InlineData("   ", "foo")]
    public void StringConstructor_Throws_OnInvalidArgs(string name, string value)
    {
        // assert
        Assert.Throws<ArgumentException>(() => new ProfileProperty(name, value, "sig"));
    }

    [Theory]
    [InlineData(@"{""name"":""foo"",""value"":""bar""}", "name", "value", "signature", "foo", "bar", null)]
    [InlineData(@"{""name"":""bar"",""value"":""baz"",""signature"":""sig""}", "name", "value", "signature", "bar", "baz", "sig")]
    public void JsonConstructor_Sets_Members(string json, string nameKey, string valueKey, string sigKey, string expectedName, string expectedValue, string expectedSig)
    {
        // arrange
        using var doc = JsonDocument.Parse(json);

        // act
        var property = new ProfileProperty(doc.RootElement, nameKey, valueKey, sigKey);

        // assert
        Assert.Equal(expectedName, property.Name);
        Assert.Equal(expectedValue, property.Value);
        Assert.Equal(expectedSig, property.Signature);
    }

    [Theory]
    [InlineData(@"{}")]
    [InlineData(@"{""name"":""foo"",""value"":""""}")]
    [InlineData(@"{""name"":"""",""value"":""bar""}")]
    [InlineData(@"{""name"":""foo"",""value"":""   ""}")]
    [InlineData(@"{""name"":""   "",""value"":""bar""}")]
    public void JsonConstructor_Throws_OnInvalidArgs(string json)
    {
        // arrange
        using var doc = JsonDocument.Parse(json);

        // act, assert
        Assert.Throws<ArgumentException>(() => new ProfileProperty(doc.RootElement));
    }

    [Theory]
    [InlineData("Zm9v", "foo")]
    [InlineData("invalid", null)]
    public void GetDecodedValue_Returns_CorrectString(string value, string expectedDecoded)
    {
        // arrange
        var property = new ProfileProperty("foo", value);

        // act
        var decoded = property.GetDecodedValue();

        // assert
        Assert.Equal(expectedDecoded, decoded);
    }
}

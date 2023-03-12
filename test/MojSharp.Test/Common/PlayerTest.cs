using System.Text.Json;
using MojSharp.Common;
using Xunit;

namespace MojSharp.Test.Common;

/// <summary>
/// Unit tests for <see cref="Player"/>.
/// </summary>
public class PlayerTest
{
    [Theory]
    [InlineData("foo", "bar")]
    public void StringConstructor_Sets_Members(string username, string uuid)
    {
        // act
        var player = new Player(username, uuid);

        // assert
        Assert.Equal(username, player.Username);
        Assert.Equal(uuid, player.Uuid);
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
    public void StringConstructor_Throws_OnInvalidArgs(string username, string uuid)
    {
        // assert
        Assert.Throws<ArgumentException>(() => new Player(username, uuid));
    }

    [Theory]
    [InlineData(@"{""name"":""foo"",""id"":""bar""}", "name", "id", "foo", "bar")]
    [InlineData(@"{""foo"":""name"",""bar"":""id""}", "foo", "bar", "name", "id")]
    public void JsonConstructor_Sets_Members(string json, string nameKey, string idKey, string expectedName, string expectedId)
    {
        // arrange
        using var doc = JsonDocument.Parse(json);

        // act
        var player = new Player(doc.RootElement, nameKey, idKey);

        // assert
        Assert.Equal(expectedName, player.Username);
        Assert.Equal(expectedId, player.Uuid);
    }

    [Theory]
    [InlineData(@"{}")]
    [InlineData(@"{""name"":""foo"", ""id"":""""}")]
    [InlineData(@"{""name"":"""", ""id"":""bar""}")]
    [InlineData(@"{""name"":""foo"", ""id"":""   ""}")]
    [InlineData(@"{""name"":""   "", ""id"":""bar""}")]
    public void JsonConstructor_Throws_OnInvalidArgs(string json)
    {
        // arrange
        using var doc = JsonDocument.Parse(json);

        // act, assert
        Assert.Throws<ArgumentException>(() => new Player(doc.RootElement));
    }
}

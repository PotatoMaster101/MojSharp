using System.Text.Json;
using MojSharp.Common;
using Xunit;

namespace MojSharp.Test.Common;

/// <summary>
/// Unit tests for <see cref="Skin"/>.
/// </summary>
public class SkinTest
{
    [Theory]
    [InlineData(@"{""url"":""foo""}", false, "foo", null, false, false)]
    [InlineData(@"{""url"":""bar"",""metadata"":{""model"":""slim""}}", false, "bar", null, true, false)]
    [InlineData(@"{""id"":""foo"",""url"":""bar"",""state"":""ACTIVE"",""variant"":""CLASSIC""}", true, "bar", "foo", false, true)]
    [InlineData(@"{""id"":""foo"",""url"":""bar"",""state"":""ACTIVE"",""variant"":""SLIM""}", true, "bar", "foo", true, true)]
    [InlineData(@"{""id"":""foo"",""url"":""bar"",""variant"":""SLIM""}", true, "bar", "foo", true, false)]
    public void Constructor_Sets_Members(string json, bool auth, string expectedUrl, string expectedId, bool expectedSlim, bool expectedActive)
    {
        // arrange
        using var doc = JsonDocument.Parse(json);

        // act
        var skin = new Skin(doc.RootElement, auth);

        // assert
        Assert.Equal(expectedUrl, skin.Url);
        Assert.Equal(expectedId, skin.Id);
        Assert.Equal(expectedSlim, skin.Slim);
        Assert.Equal(expectedActive, skin.Active);
    }

    [Theory]
    [InlineData("{}")]
    [InlineData(@"{""url"":""""}")]
    [InlineData(@"{""url"":""   ""}")]
    [InlineData(@"{""url"":10}")]
    [InlineData(@"{""id"":""foo"",""url"":""bar""}", true)]
    [InlineData(@"{""url"":""bar"",""variant"":""SLIM""}", true)]
    public void Constructor_Throws_OnInvalidArgs(string json, bool auth = false)
    {
        // arrange
        using var doc = JsonDocument.Parse(json);

        // act, assert
        Assert.Throws<ArgumentException>(() => new Skin(doc.RootElement, auth));
    }
}

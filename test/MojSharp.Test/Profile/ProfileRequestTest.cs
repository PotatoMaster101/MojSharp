using MojSharp.Exception;
using MojSharp.Profile;
using Xunit;

namespace MojSharp.Test.Profile;

/// <summary>
/// Unit tests for <see cref="ProfileRequest"/>.
/// </summary>
public class ProfileRequestTest
{
    [Theory]
    [InlineData("foo", true)]
    [InlineData("foo", false)]
    public void Constructor_Sets_Members(string uuid, bool unsigned)
    {
        // act
        var request = new ProfileRequest(uuid, unsigned);

        // assert
        Assert.Equal($"https://sessionserver.mojang.com/session/minecraft/profile/{uuid}{(unsigned ? "" : "?unsigned=false")}", request.Address.OriginalString);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Throws_OnInvalidArgs(string uuid)
    {
        // assert
        Assert.Throws<ArgumentException>(() => new ProfileRequest(uuid));
    }

    [Theory]
    [InlineData("8b57078bf1bd45df83c4d88d16768fbe", "MHF_Pig", true)]
    [InlineData("8b57078bf1bd45df83c4d88d16768fbe", "MHF_Pig", false)]
    public async Task Request_Returns_CorrectHistories(string uuid, string expectedName, bool unsigned)
    {
        // arrange
        var request = new ProfileRequest(uuid, unsigned);

        // act
        var response = await request.Request();

        // assert
        Assert.True(response.RawData.Length > 0);
        Assert.True(response.Properties.Count > 0);
        Assert.Equal(uuid, response.Player.Uuid);
        Assert.Equal(expectedName, response.Player.Username);
        // First() throws when not found, failing the test
        _ = response.Properties.First(x => x.Name is "textures");
        if (!unsigned)
            _ = response.Properties.First(x => x.Signature is not null);
    }

    [Theory]
    [InlineData("invalid")]
    public async Task Request_Throws_OnInvalidRepsonse(string uuid)
    {
        // arrange
        var request = new ProfileRequest(uuid);

        // act, assert
        await Assert.ThrowsAsync<InvalidResponseException>(() => request.Request());
    }
}

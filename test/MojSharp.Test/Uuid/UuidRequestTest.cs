using MojSharp.Exception;
using MojSharp.Uuid;
using Xunit;

namespace MojSharp.Test.Uuid;

/// <summary>
/// Unit tests for <see cref="UuidRequest"/>.
/// </summary>
public class UuidRequestTest
{
    [Theory]
    [InlineData("Notch")]
    public void Constructor_Sets_Members(string username)
    {
        // act
        var request = new UuidRequest(username);

        // assert
        Assert.Equal($"https://api.mojang.com/users/profiles/minecraft/{username}", request.Address.OriginalString);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Throws_OnInvalidArgs(string username)
    {
        // assert
        Assert.Throws<ArgumentException>(() => new UuidRequest(username));
    }

    [Theory]
    [InlineData("Notch", "069a79f444e94726a5befca90e38aaf5")]
    public async Task Request_Returns_CorrectResponse(string username, string expectedUuid)
    {
        // arrange
        var request = new UuidRequest(username);

        // act
        var response = await request.Request();

        // assert
        Assert.True(response.RawData.Length > 0);
        Assert.Equal(username, response.Player.Username);
        Assert.Equal(expectedUuid, response.Player.Uuid);
    }

    [Theory]
    [InlineData("a")]
    [InlineData("ThisUsernameIsTooDamnLong123456789")]
    public async Task Request_Throws_OnInvalidResponse(string username)
    {
        // arrange
        var request = new UuidRequest(username);

        // act, assert
        await Assert.ThrowsAsync<InvalidResponseException>(() => request.Request());
    }
}

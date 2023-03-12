using MojSharp.BlockedServer;
using Xunit;

namespace MojSharp.Test.BlockedServer;

/// <summary>
/// Unit tests for <see cref="BlockedServerRequest"/>.
/// </summary>
public class BlockedServerRequestTest
{
    [Fact]
    public void Constructor_Sets_Members()
    {
        // act
        var request = new BlockedServerRequest();

        // assert
        Assert.Equal("https://sessionserver.mojang.com/blockedservers", request.Address.OriginalString);
    }

    [Fact]
    public async Task Request_Returns_CorrectResponse()
    {
        // arrange
        var request = new BlockedServerRequest();

        // act
        var response = await request.Request();

        // assert
        Assert.True(response.RawData.Length > 0);
        Assert.True(response.Hashes.Count > 0);
    }
}

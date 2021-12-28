using System;
using System.Linq;
using System.Threading.Tasks;
using MojSharp.Exception;
using MojSharp.Name;
using Xunit;

namespace MojSharp.Test.Name;

/// <summary>
/// Unit tests for <see cref="NameHistoryRequest"/>.
/// </summary>
public class NameHistoryRequestTest
{
    [Theory]
    [InlineData("foo")]
    public void Constructor_Sets_Members(string uuid)
    {
        // arrange, act
        var request = new NameHistoryRequest(uuid);

        // assert
        Assert.Equal($"https://api.mojang.com/user/profiles/{uuid}/names", request.Address.OriginalString);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Throws_OnInvalidArgs(string uuid)
    {
        // arrange, act, assert
        Assert.Throws<ArgumentException>(() => new NameHistoryRequest(uuid));
    }

    [Theory]
    [InlineData("cb2671d590b84dfe9b1c73683d451d1a", "PotatoMaster101")]
    [InlineData("79217508a0484a78abba52c66049f028", "SxmplyKathie_", "__Kathleen__", "KathLovesBigBang")]
    public async Task Request_Returns_CorrectResponse(string uuid, params string[] histories)
    {
        // arrange
        var request = new NameHistoryRequest(uuid);

        // act
        var response = await request.Request();

        // assert
        Assert.True(response.RawData.Length > 0);
        Assert.True(response.Histories.Count >= histories.Length);
        foreach (var name in histories)
        {
            // First() throws when not found, failing the test
            _ = response.Histories.First(x => x.Username == name);
        }
    }

    [Theory]
    [InlineData("foo")]
    public async Task Request_Throws_OnInvalidResponse(string uuid)
    {
        // arrange
        var request = new NameHistoryRequest(uuid);

        // act, assert
        await Assert.ThrowsAsync<InvalidResponseException>(() => request.Request());
    }
}

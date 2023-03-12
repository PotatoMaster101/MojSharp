using System.Collections;
using MojSharp.Common;
using MojSharp.Exception;
using MojSharp.Uuid;
using Xunit;

namespace MojSharp.Test.Uuid;

/// <summary>
/// Unit tests for <see cref="MultiUuidRequest"/>.
/// </summary>
public class MultiUuidRequestTest
{
    [Theory]
    [InlineData(@"[""""]")]
    [InlineData(@"[""foo""]", "foo")]
    [InlineData(@"[""foo"",""bar""]", "foo", "bar")]
    public void Constructor_Sets_Members(string expectedPostData, params string[] usernames)
    {
        // act
        var request = new TestRequest(usernames);

        // assert
        Assert.Equal("https://api.mojang.com/profiles/minecraft", request.Address.OriginalString);
        Assert.NotNull(request.GetPostData());
        Assert.Equal(expectedPostData, request.GetPostData());
    }

    [Fact]
    public void Constructor_Throws_OnInvalidArgs()
    {
        // arrange , act, assert
        Assert.Throws<ArgumentException>(() => new TestRequest(new[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"}));
    }

    [Theory, ClassData(typeof(RequestReturnsCorrectPlayerTestData))]
    public async Task Request_Returns_CorrectPlayers(string[] usernames, Player[] expectedPlayers)
    {
        // arrange
        var request = new MultiUuidRequest(usernames);

        // act
        var response = await request.Request();

        // assert
        Assert.True(response.RawData.Length > 0);
        Assert.Equal(expectedPlayers.Length, response.Players.Count);
        foreach (var player in expectedPlayers)
        {
            // First() throws when not found, failing the test
            _ = response.Players.First(x => x.Username == player.Username && x.Uuid == player.Uuid);
        }
    }

    [Theory]
    [InlineData("Notch", "ThisUsernameIsWayTooLong123456789")]
    public async Task Request_Throws_OnInvalidResponse(params string[] usernames)
    {
        // arrange
        var request = new MultiUuidRequest(usernames);

        // act, assert
        await Assert.ThrowsAsync<InvalidResponseException>(() => request.Request());
    }

    /// <summary>
    /// Fake request for unit testing.
    /// </summary>
    private class TestRequest : MultiUuidRequest
    {
        public TestRequest(IReadOnlyCollection<string> usernames)
            : base(usernames) { }

        public string? GetPostData() => PostData;
    }

    /// <summary>
    /// Test data for <see cref="Request_Returns_CorrectPlayers(string[], Player[])"/>.
    /// </summary>
    private class RequestReturnsCorrectPlayerTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new[] { "Notch", "Dinnerbone" }, new Player[] { new("Notch", "069a79f444e94726a5befca90e38aaf5"), new("Dinnerbone", "61699b2ed3274a019f1e0ea8c3f06bc6") } };
        }
    }
}

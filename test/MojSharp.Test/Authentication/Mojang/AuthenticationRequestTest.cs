using MojSharp.Authentication.Mojang;
using MojSharp.Exception;
using Xunit;

namespace MojSharp.Test.Authentication.Mojang;

/// <summary>
/// Unit tests for <see cref="AuthenticationRequest"/>.
/// </summary>
public class AuthenticationRequestTest
{
    [Theory]
    [InlineData("foo", "bar", "baz")]
    public void Constructor_Sets_Members(string username, string password, string clientToken)
    {
        // arrange
        const string agent = @"""agent"":{""name"":""Minecraft"",""Version"":1}";

        // act
        var request = new TestRequest(username, password, clientToken);

        // assert
        Assert.NotNull(request.GetPostData());
        Assert.Equal($@"{{{agent},""username"":""{username}"",""password"":""{password}"",""clientToken"":""{clientToken}""}}", request.GetPostData());
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
    public void Constructor_Throws_OnInvalidArgs(string username, string password)
    {
        // assert
        Assert.Throws<ArgumentException>(() => new AuthenticationRequest(username, password));
    }

    [SkippableFact]
    public async Task Request_Returns_CorrectResponse()
    {
        // arrange
        var email = Environment.GetEnvironmentVariable("MOJANG_EMAIL");
        var username = Environment.GetEnvironmentVariable("MOJANG_USERNAME");
        var password = Environment.GetEnvironmentVariable("MOJANG_PASSWORD");

        var skip = string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(username);
        Skip.If(skip, "Authentication environment variables are not set");
        var request = new AuthenticationRequest(email!, password!);

        // act
        var response = await request.Request();

        // assert
        Assert.True(response.RawData.Length > 0);
        Assert.Equal(username, response.Profile.Username);
        Assert.True(response.ClientToken.Length > 0);
        Assert.True(response.AccessToken.Length > 0);
    }

    [Theory]
    [InlineData("a", "b")]
    public async Task Request_Throws_OnInvalidResponse(string username, string password)
    {
        // arrange
        var request = new AuthenticationRequest(username, password);

        // act, assert
        await Assert.ThrowsAsync<InvalidResponseException>(() => request.Request());
    }

    /// <summary>
    /// Fake request for unit testing.
    /// </summary>
    private class TestRequest : AuthenticationRequest
    {
        public TestRequest(string username, string password, string? clientToken = null)
            : base(username, password, clientToken) { }

        public string? GetPostData() => PostData;
    }
}

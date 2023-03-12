using MojSharp.Authentication.Mojang;
using MojSharp.Common;
using Xunit;

namespace MojSharp.Test.Authentication.Mojang;

/// <summary>
/// Unit tests for <see cref="AuthenticationResponse"/>.
/// </summary>
public class AuthenticationResponseTest
{
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
    public void Constructor_Throws_OnInvalidArgs(string client, string access)
    {
        // assert
        Assert.Throws<ArgumentException>(() => new AuthenticationResponse("foo", client, access, new Player("foo", "bar")));
    }
}

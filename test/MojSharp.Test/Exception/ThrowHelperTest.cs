using System.Net;
using MojSharp.Exception;
using Xunit;

namespace MojSharp.Test.Exception;

/// <summary>
/// Unit tests for <see cref="ThrowHelper"/>.
/// </summary>
public class ThrowHelperTest
{
    [Theory]
    [InlineData("{}", HttpStatusCode.NotFound)]
    [InlineData("", HttpStatusCode.NotFound)]
    [InlineData(null, HttpStatusCode.NotFound)]
    public void ThrowCauseException_Throws_InvalidCauseException(string json, HttpStatusCode status)
    {
        // assert
        Assert.Throws<InvalidCauseException>(() => ThrowHelper.ThrowCauseException(json, status));
    }

    [Theory]
    [InlineData("{}", HttpStatusCode.NotImplemented)]
    [InlineData("", HttpStatusCode.NotImplemented)]
    [InlineData(null, HttpStatusCode.NotImplemented)]
    public void ThrowPathException_Throws_InvalidPathException(string json, HttpStatusCode status)
    {
        // assert
        Assert.Throws<InvalidPathException>(() => ThrowHelper.ThrowPathException(json, status));
    }

    [Theory]
    [InlineData("{}", HttpStatusCode.Forbidden)]
    [InlineData("", HttpStatusCode.Forbidden)]
    [InlineData(null, HttpStatusCode.Forbidden)]
    public void ThrowResponseException_Throws_InvalidResponseException(string json, HttpStatusCode status)
    {
        // assert
        Assert.Throws<InvalidResponseException>(() => ThrowHelper.ThrowResponseException(json, status));
    }
}

using System.Net;
using System.Text.Json;
using MojSharp.Exception;
using Xunit;

namespace MojSharp.Test.Exception;

/// <summary>
/// Unit tests for <see cref="InvalidPathException"/>.
/// </summary>
public class InvalidPathExceptionTest
{
    [Theory]
    [InlineData(HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.Forbidden)]
    public void StatusConstructor_Sets_Members(HttpStatusCode status)
    {
        // act
        var exception = new InvalidPathException(status);

        // assert
        Assert.Equal(status, exception.Status);
        Assert.Equal("Unknown path", exception.Path);
    }

    [Theory]
    [InlineData(@"{""path"":""foo""}", HttpStatusCode.NotFound, "foo")]
    [InlineData(@"{}", HttpStatusCode.TooManyRequests, "Unknown path")]
    public void JsonConstructor_Sets_Members(string json, HttpStatusCode status, string expectedPath)
    {
        // arrange
        using var jsonDoc = JsonDocument.Parse(json);

        // act
        var exception = new InvalidPathException(jsonDoc.RootElement, status);

        // assert
        Assert.Equal(status, exception.Status);
        Assert.Equal(expectedPath, exception.Path);
    }
}

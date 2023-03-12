using System.Net;
using System.Text.Json;
using MojSharp.Exception;
using Xunit;

namespace MojSharp.Test.Exception;

/// <summary>
/// Unit tests for <see cref="InvalidCauseException"/>.
/// </summary>
public class InvalidCauseExceptionTest
{
    [Theory]
    [InlineData(HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.Forbidden)]
    public void StatusConstructor_Sets_Members(HttpStatusCode status)
    {
        // act
        var exception = new InvalidCauseException(status);

        // assert
        Assert.Equal(status, exception.Status);
        Assert.Equal("Unknown cause", exception.Cause);
    }

    [Theory]
    [InlineData(@"{""cause"":""foo""}", HttpStatusCode.NotFound, "foo")]
    [InlineData(@"{}", HttpStatusCode.TooManyRequests, "Unknown cause")]
    public void JsonConstructor_Sets_Members(string json, HttpStatusCode status, string expectedCause)
    {
        // arrange
        using var jsonDoc = JsonDocument.Parse(json);

        // act
        var exception = new InvalidCauseException(jsonDoc.RootElement, status);

        // assert
        Assert.Equal(status, exception.Status);
        Assert.Equal(expectedCause, exception.Cause);
    }
}

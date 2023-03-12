using System.Net;
using System.Text.Json;
using MojSharp.Exception;
using Xunit;

namespace MojSharp.Test.Exception;

/// <summary>
/// Unit tests for <see cref="InvalidResponseException"/>.
/// </summary>
public class InvalidResponseExceptionTest
{
    [Theory]
    [InlineData(HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.Forbidden)]
    public void StatusConstructor_Sets_Members(HttpStatusCode status)
    {
        // act
        var exception = new InvalidResponseException(status);

        // assert
        Assert.Equal(status, exception.Status);
        Assert.Equal("Error", exception.Name);
        Assert.Equal("Unknown error", exception.Content);
        Assert.Equal($"Error ({status}): Unknown error", exception.Message);
    }

    [Theory]
    [InlineData(@"{""error"":""foo"",""errorMessage"":""bar""}", HttpStatusCode.Forbidden, "foo", "bar")]
    [InlineData(@"{""errorMessage"":""bar""}", HttpStatusCode.TooManyRequests, "Error", "bar")]
    [InlineData(@"{""error"":""foo""}", HttpStatusCode.NotFound, "foo", "Unknown error")]
    [InlineData(@"{}", HttpStatusCode.NotFound, "Error", "Unknown error")]
    public void JsonConstructor_Sets_Members(string json, HttpStatusCode status, string expectedName, string expectedContent)
    {
        // arrange
        using var jsonDoc = JsonDocument.Parse(json);

        // act
        var exception = new InvalidResponseException(jsonDoc.RootElement, status);

        // assert
        Assert.Equal(status, exception.Status);
        Assert.Equal(expectedName, exception.Name);
        Assert.Equal(expectedContent, exception.Content);
        Assert.Equal($"{expectedName} ({status}): {expectedContent}", exception.Message);
    }
}

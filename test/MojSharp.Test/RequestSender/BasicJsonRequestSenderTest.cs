using System.Net;
using MojSharp.RequestSender;
using Xunit;

namespace MojSharp.Test.RequestSender;

/// <summary>
/// Unit tests for <see cref="BasicJsonRequestSender"/>.
/// </summary>
public class BasicJsonRequestSenderTest
{
    [Theory]
    [InlineData("https://google.com", HttpStatusCode.OK, "google")]
    public async Task Get_Returns_CorrectResponse(string url, HttpStatusCode expectedStatus, string expectedContains)
    {
        // arrange
        var requestSender = new BasicJsonRequestSender();

        // act
        var (status, response) = await requestSender.Get(new Uri(url));

        // assert
        Assert.Equal(expectedStatus, status);
        Assert.Contains(expectedContains, response);
    }

    [Theory]
    [InlineData("https://google.com", HttpStatusCode.MethodNotAllowed, "google")]
    public async Task Post_Returns_CorrectResponse(string url, HttpStatusCode expectedStatus, string expectedContains)
    {
        // arrange
        var requestSender = new BasicJsonRequestSender();

        // act
        var (status, response) = await requestSender.Post(new Uri(url), "");

        // assert
        Assert.Equal(expectedStatus, status);
        Assert.Contains(expectedContains, response);
    }
}

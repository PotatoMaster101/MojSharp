using MojSharp.Request;
using MojSharp.RequestSender;
using MojSharp.Response;
using Xunit;

namespace MojSharp.Test.Request;

/// <summary>
/// Unit tests for <see cref="BaseRequest{T}"/>.
/// </summary>
public class BaseRequestTest
{
    [Theory]
    [InlineData("https://google.com")]
    public void Constructor_Sets_Members(string url)
    {
        // arrange
        var sender = new BasicJsonRequestSender();

        // act
        var request = new TestRequest(sender, new Uri(url));

        // assert
        Assert.Same(sender, request.GetSender());
        Assert.Equal(url, request.Address.OriginalString);
    }

    /// <summary>
    /// Fake request for unit testing.
    /// </summary>
    private class TestRequest : BaseRequest<IResponse>
    {
        public TestRequest(IRequestSender sender, Uri address)
            : base(sender, address) { }

        public IRequestSender GetSender() => RequestSender;
        public override Task<IResponse> Request(CancellationToken cancellation = default) => throw new NotImplementedException();
    }
}

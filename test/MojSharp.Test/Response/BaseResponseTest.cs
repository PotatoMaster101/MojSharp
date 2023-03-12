using MojSharp.Response;
using Xunit;

namespace MojSharp.Test.Response;

/// <summary>
/// Unit tests for <see cref="BaseResponse"/>.
/// </summary>
public class BaseResponseTest
{
    [Theory]
    [InlineData("data")]
    public void Constructor_Sets_Members(string rawData)
    {
        // act
        var response = new BaseResponse(rawData);

        // assert
        Assert.Equal(rawData, response.RawData);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Throws_OnInvalidArgs(string rawData)
    {
        // assert
        Assert.Throws<ArgumentException>(() => new BaseResponse(rawData));
    }
}

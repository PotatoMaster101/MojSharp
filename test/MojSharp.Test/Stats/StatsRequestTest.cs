using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MojSharp.RequestSender;
using MojSharp.Stats;
using Xunit;

namespace MojSharp.Test.Stats;

/// <summary>
/// Unit tests for <see cref="StatsRequest"/>.
/// </summary>
public class StatsRequestTest
{
    [Theory]
    [InlineData(@"{""metricKeys"":[""""]}")]
    [InlineData(@"{""metricKeys"":[""item_sold_minecraft""]}", MetricKey.ItemSoldMinecraft)]
    [InlineData(@"{""metricKeys"":[""item_sold_cobalt"",""item_sold_dungeons""]}", MetricKey.ItemSoldCobalt, MetricKey.ItemSoldDungeons)]
    public void Constructor_Sets_Members(string expectedPost, params MetricKey[] keys)
    {
        // arrange, act
        var request = new TestClass(keys);

        // assert
        Assert.Equal("https://api.mojang.com/orders/statistics", request.Address.OriginalString);
        Assert.NotNull(request.GetPostData);
        Assert.Equal(expectedPost, request.GetPostData);
    }

    [Fact]
    public void Constructor_Throws_OnNullArgs()
    {
        // arrange, act, assert
        Assert.Throws<ArgumentNullException>(() => new StatsRequest(null!));
    }

    [Theory]
    [InlineData(MetricKey.ItemSoldMinecraft)]
    [InlineData(MetricKey.ItemSoldCobalt, MetricKey.ItemSoldDungeons)]
    [InlineData((MetricKey)999, (MetricKey)998)]
    public async Task Request_Returns_CorrectResponse(params MetricKey[] keys)
    {
        // arrange
        var request = new StatsRequest(keys);

        // act
        var response = await request.Request();

        // assert
        Assert.True(response.RawData.Length > 0);
        Assert.True(response.Total >= 0);
        Assert.True(response.Last24H >= 0);
        Assert.True(response.SalePerSecond >= 0);
    }

    /// <summary>
    /// Fake class for testing <see cref="StatsRequest"/>.
    /// </summary>
    private class TestClass : StatsRequest
    {
        public TestClass(IReadOnlyCollection<MetricKey> metrics)
            : base(new BasicJsonRequestSender(), metrics) { }

        public string? GetPostData => PostData;
    }
}

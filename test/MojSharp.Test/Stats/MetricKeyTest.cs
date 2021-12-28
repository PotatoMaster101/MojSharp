using MojSharp.Stats;
using Xunit;

namespace MojSharp.Test.Stats;

/// <summary>
/// Unit tests for <see cref="MetricKey"/> and <see cref="MetricKeyHelper"/>.
/// </summary>
public class MetricKeyTest
{
    [Theory]
    [InlineData(MetricKey.ItemSoldCobalt, "item_sold_cobalt")]
    [InlineData(MetricKey.ItemSoldDungeons, "item_sold_dungeons")]
    [InlineData(MetricKey.ItemSoldMinecraft, "item_sold_minecraft")]
    [InlineData(MetricKey.ItemSoldScrolls, "item_sold_scrolls")]
    [InlineData(MetricKey.PrepaidCardRedeemedCobalt, "prepaid_card_redeemed_cobalt")]
    [InlineData(MetricKey.PrepaidCardRedeemedMinecraft, "prepaid_card_redeemed_minecraft")]
    [InlineData((MetricKey)999, "")]
    public void ToMetricString_Returns_CorrectString(MetricKey key, string expected)
    {
        // arrange, act
        var str = key.ToMetricString();

        // assert
        Assert.Equal(expected, str);
    }

    [Theory]
    [InlineData("item_sold_cobalt", MetricKey.ItemSoldCobalt)]
    [InlineData("item_sold_dungeons", MetricKey.ItemSoldDungeons)]
    [InlineData("item_sold_minecraft", MetricKey.ItemSoldMinecraft)]
    [InlineData("item_sold_scrolls", MetricKey.ItemSoldScrolls)]
    [InlineData("prepaid_card_redeemed_cobalt", MetricKey.PrepaidCardRedeemedCobalt)]
    [InlineData("prepaid_card_redeemed_minecraft", MetricKey.PrepaidCardRedeemedMinecraft)]
    [InlineData("invalid", MetricKey.ItemSoldMinecraft)]
    [InlineData("", MetricKey.ItemSoldMinecraft)]
    public void ToMetricKey_Returns_CorrectKey(string key, MetricKey expected)
    {
        // arrange, act
        var metric = key.ToMetricKey();

        // assert
        Assert.Equal(expected, metric);
    }
}

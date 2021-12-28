namespace MojSharp.Stats;

/// <summary>
/// Available metric keys.
/// </summary>
public enum MetricKey
{
    /// <summary>
    /// Number of Cobalt sold.
    /// </summary>
    ItemSoldCobalt = 0,

    /// <summary>
    /// Number of Dungeons sold.
    /// </summary>
    ItemSoldDungeons,

    /// <summary>
    /// Number of Minecraft sold.
    /// </summary>
    ItemSoldMinecraft,

    /// <summary>
    /// Number of Scrolls sold.
    /// </summary>
    ItemSoldScrolls,

    /// <summary>
    /// Number of Cobalt redeemed.
    /// </summary>
    PrepaidCardRedeemedCobalt,

    /// <summary>
    /// Number of Minecraft redeemed.
    /// </summary>
    PrepaidCardRedeemedMinecraft
}

/// <summary>
/// Helper for <see cref="MetricKey"/> conversions.
/// </summary>
public static class MetricKeyHelper
{
    /// <summary>
    /// Returns the string representation of a <see cref="MetricKey"/>.
    /// </summary>
    /// <param name="key">The key to convert.</param>
    /// <returns>The string representation of a <see cref="MetricKey"/>, or <see cref="string.Empty"/> if not found.</returns>
    public static string ToMetricString(this MetricKey key)
    {
        return key switch
        {
            MetricKey.ItemSoldCobalt => "item_sold_cobalt",
            MetricKey.ItemSoldDungeons => "item_sold_dungeons",
            MetricKey.ItemSoldMinecraft => "item_sold_minecraft",
            MetricKey.ItemSoldScrolls => "item_sold_scrolls",
            MetricKey.PrepaidCardRedeemedCobalt => "prepaid_card_redeemed_cobalt",
            MetricKey.PrepaidCardRedeemedMinecraft => "prepaid_card_redeemed_minecraft",
            _ => string.Empty
        };
    }

    /// <summary>
    /// Converts the given string to a <see cref="MetricKey"/>.
    /// </summary>
    /// <param name="key">The string to convert.</param>
    /// <returns>The converted <see cref="MetricKey"/>. If not found, <see cref="MetricKey.ItemSoldMinecraft"/> will be returned.</returns>
    public static MetricKey ToMetricKey(this string key)
    {
        return key switch
        {
            "item_sold_cobalt" => MetricKey.ItemSoldCobalt,
            "item_sold_dungeons" => MetricKey.ItemSoldDungeons,
            "item_sold_minecraft" => MetricKey.ItemSoldMinecraft,
            "item_sold_scrolls" => MetricKey.ItemSoldScrolls,
            "prepaid_card_redeemed_cobalt" => MetricKey.PrepaidCardRedeemedCobalt,
            "prepaid_card_redeemed_minecraft" => MetricKey.PrepaidCardRedeemedMinecraft,
            _ => MetricKey.ItemSoldMinecraft
        };
    }
}

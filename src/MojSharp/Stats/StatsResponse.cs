using MojSharp.Response;

namespace MojSharp.Stats;

/// <summary>
/// A response from the statistics endpoint.
/// </summary>
public class StatsResponse : BaseResponse
{
    /// <summary>
    /// Gets the total number of items sold.
    /// </summary>
    /// <value>The total number of items sold.</value>
    public long Total { get; }

    /// <summary>
    /// Gets the number of items sold in the last 24h.
    /// </summary>
    /// <value>The number of items sold in the last 24h.</value>
    public long Last24H { get; }

    /// <summary>
    /// Gets the average sales per second.
    /// </summary>
    /// <value>The average sales per second.</value>
    public double SalePerSecond { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="StatsResponse"/>.
    /// </summary>
    /// <param name="rawData">The raw data of the response.</param>
    /// <param name="total">Total number of items sold.</param>
    /// <param name="last24H">Number of items sold in the last 24h.</param>
    /// <param name="salePerSecond">Average sales per second.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="rawData"/> is <see langword="null"/> or whitespace.</exception>
    public StatsResponse(string rawData, long total, long last24H, double salePerSecond)
        : base(rawData)
    {
        Total = total;
        Last24H = last24H;
        SalePerSecond = salePerSecond;
    }
}

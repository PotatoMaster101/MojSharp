using System.Text.Json;
using MojSharp.Request;
using MojSharp.RequestSender;

namespace MojSharp.Stats;

/// <summary>
/// A request to the statistics endpoint.
/// </summary>
public class StatsRequest : BaseRequest<StatsResponse>
{
    /// <summary>
    /// Constructs a new instance of <see cref="StatsRequest"/>.
    /// </summary>
    /// <param name="metrics">The list of metrics to retrieve.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="metrics"/> is <see langword="null"/>.</exception>
    public StatsRequest(IReadOnlyCollection<MetricKey> metrics)
        : this(new BasicJsonRequestSender(), metrics) { }

    /// <summary>
    /// Constructs a new instance of <see cref="StatsRequest"/>.
    /// </summary>
    /// <param name="sender">The HTTP request sender to use.</param>
    /// <param name="metrics">The list of metrics to retrieve.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="sender"/> or <paramref name="metrics"/> is <see langword="null"/>.</exception>
    public StatsRequest(IRequestSender sender, IReadOnlyCollection<MetricKey> metrics)
        : base(sender, new Uri("https://api.mojang.com/orders/statistics"))
    {
        if (metrics is null)
            throw new ArgumentNullException(nameof(metrics));

        PostData = $@"{{""metricKeys"":[""{string.Join(@""",""", metrics.Select(x => x.ToMetricString()))}""]}}";
    }

    /// <inheritdoc cref="BaseRequest{T}.Request(CancellationToken)"/>
    public override async Task<StatsResponse> Request(CancellationToken cancellation = default)
    {
        var (_, response) = await RequestSender.Post(Address, PostData!, null, cancellation).ConfigureAwait(false);
        using var doc = JsonDocument.Parse(response);
        var checkTotal = doc.RootElement.TryGetProperty("total", out var total);
        var checkLast24H = doc.RootElement.TryGetProperty("last24h", out var last24H);
        var checkSales = doc.RootElement.TryGetProperty("saleVelocityPerSeconds", out var sales);
        return new StatsResponse(response, checkTotal ? total.GetInt64() : 0, checkLast24H ? last24H.GetInt64() : 0, checkSales ? sales.GetDouble() : 0);
    }
}

using MojSharp.Exception;
using System.Net;
using MojSharp.Request;
using MojSharp.RequestSender;
using System.Text.Json;

namespace MojSharp.Name;

/// <summary>
/// A request to the name history endpoint.
/// </summary>
public class NameHistoryRequest : BaseRequest<NameHistoryResponse>
{
    /// <summary>
    /// Constructs a new instance of <see cref="NameHistoryRequest"/>.
    /// </summary>
    /// <param name="uuid">The player UUID to retrieve the name history.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="uuid"/> is <see langword="null"/> or whitespace.</exception>
    public NameHistoryRequest(string uuid)
        : this(new BasicJsonRequestSender(), uuid) { }

    /// <summary>
    /// Constructs a new instance of <see cref="NameHistoryRequest"/>.
    /// </summary>
    /// <param name="sender">The HTTP request sender to use.</param>
    /// <param name="uuid">The player UUID to retrieve the name history.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="uuid"/> is <see langword="null"/> or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="sender"/> is <see langword="null"/>.</exception>
    public NameHistoryRequest(IRequestSender sender, string uuid)
        : base(sender, new Uri($"https://api.mojang.com/user/profiles/{uuid}/names"))
    {
        if (string.IsNullOrWhiteSpace(uuid))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(uuid));
    }

    /// <inheritdoc cref="BaseRequest{T}.Request(CancellationToken)"/>
    /// <exception cref="InvalidResponseException">Thrown when response is invalid.</exception>
    public override async Task<NameHistoryResponse> Request(CancellationToken cancellation = default)
    {
        var (status, response) = await RequestSender.Get(Address, cancellation).ConfigureAwait(false);
        if (status is not HttpStatusCode.OK)
            ThrowHelper.ThrowResponseException(response, status);

        var histories = new List<NameHistory>();
        using var doc = JsonDocument.Parse(response);
        for (var i = 0; i < doc.RootElement.GetArrayLength(); i++)
            histories.Add(new NameHistory(doc.RootElement[i]));
        return new NameHistoryResponse(response, histories);
    }
}

using System.Net;
using System.Text.Json;
using MojSharp.Common;
using MojSharp.Exception;
using MojSharp.Request;
using MojSharp.RequestSender;

namespace MojSharp.Uuid;

/// <summary>
/// A request to the UUID endpoint.
/// </summary>
public class UuidRequest : BaseRequest<UuidResponse>
{
    /// <summary>
    /// Constructs a new instance of <see cref="UuidRequest"/>.
    /// </summary>
    /// <param name="username">The player username to retrieve the UUID.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> is <see langword="null"/> or whitespace.</exception>
    public UuidRequest(string username)
        : this(new BasicJsonRequestSender(), username) { }

    /// <summary>
    /// Constructs a new instance of <see cref="UuidRequest"/>.
    /// </summary>
    /// <param name="sender">The HTTP request sender to use.</param>
    /// <param name="username">The player username to retrieve the UUID.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> is <see langword="null"/> or whitespace.</exception>
    public UuidRequest(IRequestSender sender, string username)
        : base(sender, new Uri($"https://api.mojang.com/users/profiles/minecraft/{username}"))
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(username));
    }

    /// <inheritdoc cref="BaseRequest{T}.Request(CancellationToken)"/>
    /// <exception cref="InvalidResponseException">Thrown when response is invalid.</exception>
    public override async Task<UuidResponse> Request(CancellationToken cancellation = default)
    {
        var (status, response) = await RequestSender.Get(Address, cancellation).ConfigureAwait(false);
        if (status is not HttpStatusCode.OK)
            ThrowHelper.ThrowResponseException(response, status);

        using var doc = JsonDocument.Parse(response);
        return new UuidResponse(response, new Player(doc.RootElement));
    }
}

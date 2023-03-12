using System.Net;
using System.Text.Json;
using MojSharp.Common;
using MojSharp.Exception;
using MojSharp.Request;
using MojSharp.RequestSender;

namespace MojSharp.Profile;

/// <summary>
/// A request to the profile endpoint.
/// </summary>
public class ProfileRequest : BaseRequest<ProfileResponse>
{
    /// <summary>
    /// Constructs a new instance of <see cref="ProfileRequest"/>.
    /// </summary>
    /// <param name="uuid">The player UUID to retrieve the profile.</param>
    /// <param name="unsigned">If set to <see langword="false"/>, the response will contain the private key signature.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="uuid"/> is <see langword="null"/> or whitespace.</exception>
    public ProfileRequest(string uuid, bool unsigned = true)
        : this(new BasicJsonRequestSender(), uuid, unsigned) { }

    /// <summary>
    /// Constructs a new instance of <see cref="ProfileRequest"/>.
    /// </summary>
    /// <param name="sender">The HTTP request sender to use.</param>
    /// <param name="uuid">The player UUID to retrieve the profile.</param>
    /// <param name="unsigned">If set to <see langword="false"/>, the response will contain the private key signature.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="uuid"/> is <see langword="null"/> or whitespace.</exception>
    public ProfileRequest(IRequestSender sender, string uuid, bool unsigned = true)
        : base(sender, new Uri($"https://sessionserver.mojang.com/session/minecraft/profile/{uuid}{(unsigned ? "" : "?unsigned=false")}"))
    {
        if (string.IsNullOrWhiteSpace(uuid))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(uuid));
    }

    /// <inheritdoc cref="BaseRequest{T}.Request(CancellationToken)"/>
    /// <exception cref="InvalidResponseException">Thrown when response is invalid.</exception>
    public override async Task<ProfileResponse> Request(CancellationToken cancellation = default)
    {
        var (status, response) = await RequestSender.Get(Address, cancellation).ConfigureAwait(false);
        if (status is not HttpStatusCode.OK)
            ThrowHelper.ThrowResponseException(response, status);

        var properties = new List<ProfileProperty>();
        using var doc = JsonDocument.Parse(response);
        var player = new Player(doc.RootElement);
        if (doc.RootElement.TryGetProperty("properties", out var props))
            for (var i = 0; i < props.GetArrayLength(); i++)
                properties.Add(new ProfileProperty(props[i]));
        return new ProfileResponse(response, player, properties);
    }
}

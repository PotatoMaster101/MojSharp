using System.Net;
using System.Text.Json;
using MojSharp.Common;
using MojSharp.Exception;
using MojSharp.Request;
using MojSharp.RequestSender;

namespace MojSharp.Uuid;

/// <summary>
/// A request to the multiple UUID endpoint.
/// </summary>
public class MultiUuidRequest : BaseRequest<MultiUuidResponse>
{
    /// <summary>
    /// Constructs a new instance of <see cref="MultiUuidRequest"/>.
    /// </summary>
    /// <param name="usernames">The list of usernames for sending the request.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="usernames"/> has more than 10 names.</exception>
    public MultiUuidRequest(IReadOnlyCollection<string> usernames)
        : this(new BasicJsonRequestSender(), usernames) { }

    /// <summary>
    /// Constructs a new instance of <see cref="MultiUuidRequest"/>.
    /// </summary>
    /// <param name="sender">The HTTP request sender to use.</param>
    /// <param name="usernames">The list of usernames for sending the request.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="usernames"/> has more than 10 names.</exception>
    public MultiUuidRequest(IRequestSender sender, IReadOnlyCollection<string> usernames)
        : base(sender, new Uri("https://api.mojang.com/profiles/minecraft"))
    {
        if (usernames.Count > 10)
            throw new ArgumentException($"{nameof(usernames.Count)} > 10", nameof(usernames));

        PostData = @"[""" + string.Join(@""",""", usernames) + @"""]";
    }

    /// <inheritdoc cref="BaseRequest{T}.Request(CancellationToken)"/>
    /// <exception cref="InvalidResponseException">Thrown when response is invalid.</exception>
    public override async Task<MultiUuidResponse> Request(CancellationToken cancellation = default)
    {
        var (status, response) = await RequestSender.Post(Address, PostData!, null, cancellation).ConfigureAwait(false);
        if (status is not HttpStatusCode.OK)
            ThrowHelper.ThrowResponseException(response, status);

        var players = new List<Player>(10);
        using var doc = JsonDocument.Parse(response);
        for (var i = 0; i < doc.RootElement.GetArrayLength(); i++)
            players.Add(new Player(doc.RootElement[i]));
        return new MultiUuidResponse(response, players);
    }
}

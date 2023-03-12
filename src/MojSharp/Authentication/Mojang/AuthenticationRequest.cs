using System.Net;
using System.Text.Json;
using MojSharp.Common;
using MojSharp.Exception;
using MojSharp.Request;
using MojSharp.RequestSender;

namespace MojSharp.Authentication.Mojang;

/// <summary>
/// A request to the Mojang authentication endpoint.
/// </summary>
public class AuthenticationRequest : BaseRequest<AuthenticationResponse>
{
    /// <summary>
    /// Authentication agent string.
    /// </summary>
    private const string Agent = @"""agent"":{""name"":""Minecraft"",""Version"":1}";

    /// <summary>
    /// Constructs a new instance of <see cref="AuthenticationResponse"/>.
    /// </summary>
    /// <param name="username">The account username.</param>
    /// <param name="password">The account password.</param>
    /// <param name="clientToken">The client token. Generates a random one if is <see langword="null"/>.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> or <paramref name="password"/> is <see langword="null"/> or whitespace.</exception>
    public AuthenticationRequest(string username, string password, string? clientToken = null)
        : this(new BasicJsonRequestSender(), username, password, clientToken) { }

    /// <summary>
    /// Constructs a new instance of <see cref="AuthenticationResponse"/>.
    /// </summary>
    /// <param name="sender">The HTTP request sender to use.</param>
    /// <param name="username">The account username.</param>
    /// <param name="password">The account password.</param>
    /// <param name="clientToken">The client token. Generates a random one if is <see langword="null"/>.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> or <paramref name="password"/> is <see langword="null"/> or whitespace.</exception>
    public AuthenticationRequest(IRequestSender sender, string username, string password, string? clientToken = null)
        : base(sender, new Uri("https://authserver.mojang.com/authenticate"))
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(username));
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(password));
        if (string.IsNullOrWhiteSpace(clientToken))
            clientToken = Guid.NewGuid().ToString("N");

        PostData = $@"{{{Agent},""username"":""{username}"",""password"":""{password}"",""clientToken"":""{clientToken}""}}";
    }

    /// <inheritdoc cref="BaseRequest{T}.Request(CancellationToken)"/>
    /// <exception cref="InvalidResponseException">Thrown when response is invalid.</exception>
    public override async Task<AuthenticationResponse> Request(CancellationToken cancellation = default)
    {
        var (status, response) = await RequestSender.Post(Address, PostData!, null, cancellation).ConfigureAwait(false);
        if (status is not HttpStatusCode.OK)
            ThrowHelper.ThrowResponseException(response, status);

        using var doc = JsonDocument.Parse(response);
        var accessToken = doc.RootElement.GetKeyString("accessToken");
        var clientToken = doc.RootElement.GetKeyString("clientToken");
        var player = new Player(doc.RootElement.GetProperty("selectedProfile"));
        return new AuthenticationResponse(response, clientToken, accessToken, player);
    }
}

using MojSharp.Common;
using MojSharp.Response;

namespace MojSharp.Authentication.Mojang;

/// <summary>
/// A response from the Mojang authentication endpoint.
/// </summary>
public class AuthenticationResponse : BaseResponse
{
    /// <summary>
    /// Gets the client token.
    /// </summary>
    /// <value>The client token.</value>
    public string ClientToken { get; }

    /// <summary>
    /// Gets the access token.
    /// </summary>
    /// <value>The access token.</value>
    public string AccessToken { get; }

    /// <summary>
    /// Gets the player profile.
    /// </summary>
    /// <value>The player profile.</value>
    public Player Profile { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="AuthenticationResponse"/>.
    /// </summary>
    /// <param name="rawData">The raw data of the response.</param>
    /// <param name="clientToken">Client token.</param>
    /// <param name="accessToken">Access token.</param>
    /// <param name="profile">Player information.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="rawData"/> or <paramref name="clientToken"/> or <paramref name="accessToken"/> is <see langword="null"/> or whitespace.
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="profile"/> is <see langword="null"/>.</exception>
    public AuthenticationResponse(string rawData, string clientToken, string accessToken, Player profile)
        : base(rawData)
    {
        if (string.IsNullOrWhiteSpace(clientToken))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(clientToken));
        if (string.IsNullOrWhiteSpace(accessToken))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(accessToken));

        ClientToken = clientToken;
        AccessToken = accessToken;
        Profile = profile ?? throw new ArgumentNullException(nameof(profile));
    }
}

using MojSharp.Common;
using MojSharp.Response;

namespace MojSharp.Uuid;

/// <summary>
/// A response from the UUID endpoint.
/// </summary>
public class UuidResponse : BaseResponse
{
    /// <summary>
    /// Gets the player information.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="UuidResponse"/>.
    /// </summary>
    /// <param name="rawData">The raw data of the response.</param>
    /// <param name="player">The player information.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="rawData"/> is <see langword="null"/> or whitespace.</exception>
    public UuidResponse(string rawData, Player player)
        : base(rawData)
    {
        Player = player;
    }
}

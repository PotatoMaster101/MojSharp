using MojSharp.Common;
using MojSharp.Response;

namespace MojSharp.Uuid;

/// <summary>
/// A response from the multiple UUID endpoint.
/// </summary>
public class MultiUuidResponse : BaseResponse
{
    /// <summary>
    /// Gets the list of player information.
    /// </summary>
    public IReadOnlyCollection<Player> Players { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="MultiUuidResponse"/>.
    /// </summary>
    /// <param name="rawData">The raw data of the response.</param>
    /// <param name="players">The list of player information.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="rawData"/> is <see langword="null"/> or whitespace.</exception>
    public MultiUuidResponse(string rawData, IReadOnlyCollection<Player> players)
        : base(rawData)
    {
        Players = players;
    }
}

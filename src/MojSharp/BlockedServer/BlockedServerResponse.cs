using MojSharp.Response;

namespace MojSharp.BlockedServer;

/// <summary>
/// A response from the blocked servers endpoint.
/// </summary>
public class BlockedServerResponse : BaseResponse
{
    /// <summary>
    /// Gets the list of blocked server hashes.
    /// </summary>
    public IReadOnlyCollection<string> Hashes { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="BlockedServerResponse"/>.
    /// </summary>
    /// <param name="rawData">The raw data of the response.</param>
    /// <param name="hashes">The list of blocked server hashes.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="rawData"/> is <see langword="null"/> or whitespace.</exception>
    public BlockedServerResponse(string rawData, IReadOnlyCollection<string> hashes)
        : base(rawData)
    {
        Hashes = hashes;
    }
}

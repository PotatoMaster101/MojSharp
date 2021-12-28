using MojSharp.Response;

namespace MojSharp.Name;

/// <summary>
/// A response from the name history endpoint.
/// </summary>
public class NameHistoryResponse : BaseResponse
{
    /// <summary>
    /// Gets the list of name histories.
    /// </summary>
    /// <value>The list of name histories.</value>
    public IReadOnlyCollection<NameHistory> Histories { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="NameHistoryResponse"/>.
    /// </summary>
    /// <param name="rawData">The raw data of the response.</param>
    /// <param name="histories">The list of name histories.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="rawData"/> is <see langword="null"/> or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="histories"/> is <see langword="null"/>.</exception>
    public NameHistoryResponse(string rawData, IReadOnlyCollection<NameHistory> histories)
        : base(rawData)
    {
        Histories = histories ?? throw new ArgumentNullException(nameof(histories));
    }
}

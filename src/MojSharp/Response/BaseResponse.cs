namespace MojSharp.Response;

/// <summary>
/// A base implementation for <see cref="IResponse"/>.
/// </summary>
public class BaseResponse : IResponse
{
    /// <inheritdoc cref="IResponse.RawData"/>
    public string RawData { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="BaseResponse"/>.
    /// </summary>
    /// <param name="rawData">The raw data of the response.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="rawData"/> is <see langword="null"/> or whitespace.</exception>
    public BaseResponse(string rawData)
    {
        if (string.IsNullOrWhiteSpace(rawData))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(rawData));
        RawData = rawData;
    }
}

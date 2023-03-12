namespace MojSharp.Response;

/// <summary>
/// Represents a response from the Mojang API.
/// </summary>
public interface IResponse
{
    /// <summary>
    /// Gets the raw data from the response.
    /// </summary>
    string RawData { get; }
}

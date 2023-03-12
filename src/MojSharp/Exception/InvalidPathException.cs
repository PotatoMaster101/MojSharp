using System.Net;
using System.Text.Json;

namespace MojSharp.Exception;

/// <summary>
/// Represents response errors from the Mojang API containing an error path.
/// </summary>
public class InvalidPathException : InvalidResponseException
{
    /// <summary>
    /// Default error path.
    /// </summary>
    private const string DefaultPath = "Unknown path";

    /// <summary>
    /// Gets the error path.
    /// </summary>
    public string Path { get; } = DefaultPath;

    /// <summary>
    /// Constructs a new instance of <see cref="InvalidPathException"/>.
    /// </summary>
    /// <param name="status">The error HTTP status from the API.</param>
    public InvalidPathException(HttpStatusCode status = HttpStatusCode.Forbidden)
        : base(status) { }

    /// <summary>
    /// Constructs a new instance of <see cref="InvalidPathException"/>.
    /// </summary>
    /// <param name="json">The JSON containing the error from the API.</param>
    /// <param name="status">The error HTTP status from the API.</param>
    public InvalidPathException(JsonElement json, HttpStatusCode status = HttpStatusCode.Forbidden)
        : base(json, status)
    {
        try
        {
            if (json.TryGetProperty("path", out var path))
                Path = path.GetString() ?? DefaultPath;
        }
        catch
        {
            // ignored
        }
    }
}

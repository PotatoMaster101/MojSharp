using System.Net;
using System.Text.Json;

namespace MojSharp.Exception;

/// <summary>
/// Represents response errors from the Mojang API containing an error cause.
/// </summary>
public class InvalidCauseException : InvalidResponseException
{
    /// <summary>
    /// Default error cause.
    /// </summary>
    private const string DefaultCause = "Unknown cause";

    /// <summary>
    /// Gets the error cause.
    /// </summary>
    public string Cause { get; } = DefaultCause;

    /// <summary>
    /// Constructs a new instance of <see cref="InvalidCauseException"/>.
    /// </summary>
    /// <param name="status">The error HTTP status from the API.</param>
    public InvalidCauseException(HttpStatusCode status = HttpStatusCode.Forbidden)
        : base(status) { }

    /// <summary>
    /// Constructs a new instance of <see cref="InvalidCauseException"/>.
    /// </summary>
    /// <param name="json">The JSON containing the error from the API.</param>
    /// <param name="status">The error HTTP status from the API.</param>
    public InvalidCauseException(JsonElement json, HttpStatusCode status = HttpStatusCode.Forbidden)
        : base(json, status)
    {
        try
        {
            if (json.TryGetProperty("cause", out var cause))
                Cause = cause.GetString() ?? DefaultCause;
        }
        catch
        {
            // ignored
        }
    }
}

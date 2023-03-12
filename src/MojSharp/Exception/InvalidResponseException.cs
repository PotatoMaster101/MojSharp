using System.Net;
using System.Text.Json;

namespace MojSharp.Exception;

/// <summary>
/// Represents response errors from the Mojang API.
/// </summary>
public class InvalidResponseException : System.Exception
{
    /// <summary>
    /// Default error name.
    /// </summary>
    private const string DefaultName = "Error";

    /// <summary>
    /// Default error message.
    /// </summary>
    private const string DefaultMessage = "Unknown error";

    /// <summary>
    /// Gets the error name.
    /// </summary>
    public string Name { get; } = DefaultName;

    /// <summary>
    /// Gets the error content.
    /// </summary>
    public string Content { get; } = DefaultMessage;

    /// <summary>
    /// Gets the error HTTP status code.
    /// </summary>
    public HttpStatusCode Status { get; }

    /// <inheritdoc cref="System.Exception.Message"/>
    public override string Message => $"{Name} ({Status}): {Content}";

    /// <summary>
    /// Constructs a new instance of <see cref="InvalidResponseException"/>.
    /// </summary>
    /// <param name="status">The error HTTP status from the API.</param>
    public InvalidResponseException(HttpStatusCode status = HttpStatusCode.Forbidden)
    {
        Status = status;
    }

    /// <summary>
    /// Constructs a new instance of <see cref="InvalidResponseException"/>.
    /// </summary>
    /// <param name="json">The JSON containing the error from the API.</param>
    /// <param name="status">The error HTTP status from the API.</param>
    public InvalidResponseException(JsonElement json, HttpStatusCode status = HttpStatusCode.Forbidden)
    {
        Status = status;
        try
        {
            if (json.TryGetProperty("error", out var name))
                Name = name.GetString() ?? DefaultName;
            if (json.TryGetProperty("errorMessage", out var content))
                Content = content.GetString() ?? DefaultMessage;
        }
        catch
        {
            // ignored
        }
    }
}

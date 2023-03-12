using System.Net;
using System.Text.Json;

namespace MojSharp.Exception;

/// <summary>
/// Helper for throwing response exceptions.
/// </summary>
public static class ThrowHelper
{
    /// <summary>
    /// Throws <see cref="InvalidCauseException"/>.
    /// </summary>
    /// <param name="json">The JSON string containing the error.</param>
    /// <param name="status">The HTTP status from the API.</param>
    /// <exception cref="InvalidCauseException"/>
    public static void ThrowCauseException(string json, HttpStatusCode status)
    {
        throw new InvalidCauseException(GetJson(json), status);
    }

    /// <summary>
    /// Throws <see cref="InvalidPathException"/>.
    /// </summary>
    /// <param name="json">The JSON string containing the error.</param>
    /// <param name="status">The HTTP status from the API.</param>
    /// <exception cref="InvalidPathException"/>
    public static void ThrowPathException(string json, HttpStatusCode status)
    {
        throw new InvalidPathException(GetJson(json), status);
    }

    /// <summary>
    /// Throws <see cref="InvalidResponseException"/>.
    /// </summary>
    /// <param name="json">The JSON string containing the error.</param>
    /// <param name="status">The HTTP status from the API.</param>
    /// <exception cref="InvalidResponseException"/>
    public static void ThrowResponseException(string json, HttpStatusCode status)
    {
        throw new InvalidResponseException(GetJson(json), status);
    }

    /// <summary>
    /// Converts a JSON string to a <see cref="JsonElement"/>.
    /// </summary>
    /// <param name="json">The JSON string to convert.</param>
    /// <returns>The converted <see cref="JsonElement"/>.</returns>
    private static JsonElement GetJson(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.Clone();
        }
        catch
        {
            return new JsonElement();
        }
    }
}

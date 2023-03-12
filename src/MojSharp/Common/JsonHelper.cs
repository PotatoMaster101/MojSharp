using System.Text.Json;

namespace MojSharp.Common;

/// <summary>
/// Helper for handling JSON.
/// </summary>
public static class JsonHelper
{
    /// <summary>
    /// Returns a string from the JSON using a key.
    /// </summary>
    /// <param name="json">The JSON to retrieve the string.</param>
    /// <param name="key">The key of the string.</param>
    /// <returns>The string retrieved from JSON.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="key"/> is <see langword="null"/> or whitespace, or if the retrieved string is invalid.</exception>
    public static string GetKeyString(this JsonElement json, string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(key));
        if (!json.TryGetProperty(key, out var result))
            throw new ArgumentException("Invalid JSON", nameof(json));

        try
        {
            var str = result.GetString();
            if (!string.IsNullOrWhiteSpace(str))
                return str;
        }
        catch
        {
            throw new ArgumentException("Invalid JSON", nameof(json));
        }
        throw new ArgumentException("Invalid JSON", nameof(json));
    }
}

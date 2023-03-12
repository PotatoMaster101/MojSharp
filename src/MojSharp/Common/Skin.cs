using System.Text.Json;

namespace MojSharp.Common;

/// <summary>
/// A Minecraft player skin.
/// </summary>
public class Skin
{
    /// <summary>
    /// Gets the ID for the skin. This will only appear for authenticated profile requests.
    /// </summary>
    public string? Id { get; private set; }

    /// <summary>
    /// Gets the Skin URL.
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// Gets whether the skin is slim (Alex style).
    /// </summary>
    public bool Slim { get; private set; }

    /// <summary>
    /// Gets whether the skin is active. This will only appear for authenticated profile requests.
    /// </summary>
    public bool Active { get; private set; }

    /// <summary>
    /// Constructs a new instance of <see cref="Skin"/>.
    /// </summary>
    /// <param name="json">The JSON containing the skin data.</param>
    /// <param name="authenticated">Whether the JSON is from the authenticated profile API.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="json"/> is invalid.</exception>
    public Skin(JsonElement json, bool authenticated = false)
    {
        Url = json.GetKeyString("url");
        if (authenticated)
            ParseAuthenticated(json);
        else
            ParseUnauthenticated(json);
    }

    /// <summary>
    /// Parses skin JSON from authenticated API.
    /// </summary>
    /// <param name="json">The skin JSON to parse.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="json"/> is invalid.</exception>
    private void ParseAuthenticated(JsonElement json)
    {
        Id = json.GetKeyString("id");
        Slim = json.GetKeyString("variant") is "SLIM";
        if (json.TryGetProperty("state", out var state))
            Active = state.GetString() is "ACTIVE";
    }

    /// <summary>
    /// Parses skin JSON from unauthenticated API.
    /// </summary>
    /// <param name="json">The skin JSON to parse.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="json"/> is invalid.</exception>
    private void ParseUnauthenticated(JsonElement json)
    {
        if (json.TryGetProperty("metadata", out var meta))
            Slim = meta.GetKeyString("model") is "slim";
    }
}

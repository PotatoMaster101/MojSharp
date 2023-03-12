using System.Text.Json;

namespace MojSharp.Common;

/// <summary>
/// Basic Minecraft player information.
/// </summary>
public class Player
{
    /// <summary>
    /// Gets the player username.
    /// </summary>
    public string Username { get; }

    /// <summary>
    /// Gets the player UUID.
    /// </summary>
    public string Uuid { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Player"/>.
    /// </summary>
    /// <param name="username">The player username.</param>
    /// <param name="uuid">The player UUID.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> or <paramref name="uuid"/> is <see langword="null"/> or whitespace.</exception>
    public Player(string username, string uuid)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(username));
        if (string.IsNullOrWhiteSpace(uuid))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(uuid));

        Username = username;
        Uuid = uuid;
    }

    /// <summary>
    /// Constructs a new instance of <see cref="Player"/>.
    /// </summary>
    /// <param name="json">The JSON containing player info.</param>
    /// <param name="nameKey">The key for retrieving username.</param>
    /// <param name="idKey">The key for retrieving UUID.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="json"/> is invalid.</exception>
    public Player(JsonElement json, string nameKey = "name", string idKey = "id")
    {
        Username = json.GetKeyString(nameKey);
        Uuid = json.GetKeyString(idKey);
    }
}

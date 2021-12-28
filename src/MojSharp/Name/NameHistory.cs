using System.Text.Json;
using MojSharp.Common;

namespace MojSharp.Name;

/// <summary>
/// Minecraft player name history data.
/// </summary>
public class NameHistory
{
    /// <summary>
    /// Gets the player username.
    /// </summary>
    /// <value>The player username.</value>
    public string Username { get; }

    /// <summary>
    /// Gets the name change timestamp.
    /// </summary>
    /// <value>The name change timestamp.</value>
    public long Time { get; }

    /// <summary>
    /// Gets whether this username has been changed.
    /// </summary>
    /// <value>Gets whether this username has been changed.</value>
    public bool NameChanged => Time is not 0;

    /// <summary>
    /// Constructs a new instance of <see cref="NameHistory"/>.
    /// </summary>
    /// <param name="username">The player username.</param>
    /// <param name="time">The name change timestamp.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> is <see langword="null"/> or whitespace, or <paramref name="time"/> is negative.</exception>
    public NameHistory(string username, long time = 0)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(username));
        if (time < 0)
            throw new ArgumentException($"{nameof(time)} < 0", nameof(time));

        Username = username;
        Time = time;
    }

    /// <summary>
    /// Constructs a new instance of <see cref="NameHistory"/>.
    /// </summary>
    /// <param name="json">The JSON containing player info.</param>
    /// <param name="nameKey">The key for retrieving username.</param>
    /// <param name="timeKey">The key for retrieving name change time.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="json"/> is invalid.</exception>
    public NameHistory(JsonElement json, string nameKey = "name", string timeKey = "changedToAt")
    {
        Username = json.GetKeyString(nameKey);
        if (json.TryGetProperty(timeKey, out var time))
        {
            if (time.TryGetInt64(out var value))
                Time = value;
        }
        if (Time < 0)
            throw new ArgumentException("Invalid JSON", nameof(json));
    }

    /// <summary>
    /// Returns the <see cref="DateTime"/> representation of the name change time.
    /// </summary>
    /// <returns>The <see cref="DateTime"/> representation of the name change time.</returns>
    public DateTime ToDateTime()
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(Time).DateTime;
    }
}

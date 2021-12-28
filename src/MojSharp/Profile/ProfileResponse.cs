using System.Text.Json;
using MojSharp.Common;
using MojSharp.Response;

namespace MojSharp.Profile;

/// <summary>
/// A response from the profile endpoint.
/// </summary>
public class ProfileResponse : BaseResponse
{
    /// <summary>
    /// Gets the player information.
    /// </summary>
    /// <value>The player information.</value>
    public Player Player { get; }

    /// <summary>
    /// Gets the profile properties.
    /// </summary>
    /// <value>The profile properties.</value>
    public IReadOnlyCollection<ProfileProperty> Properties { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="ProfileResponse"/>.
    /// </summary>
    /// <param name="rawData">The raw data of the response.</param>
    /// <param name="player">The player data.</param>
    /// <param name="properties">The player profile properties.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="rawData"/> is <see langword="null"/> or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="player"/> or <paramref name="properties"/> is <see langword="null"/>.</exception>
    public ProfileResponse(string rawData, Player player, IReadOnlyCollection<ProfileProperty> properties)
        : base(rawData)
    {
        Player = player ?? throw new ArgumentNullException(nameof(player));
        Properties = properties ?? throw new ArgumentNullException(nameof(properties));
    }

    /// <summary>
    /// Gets the profile texture information.
    /// </summary>
    /// <returns>The profile texture information, or <see langword="null"/> on error.</returns>
    public TextureProperty? GetTextures()
    {
        try
        {
            var texture = Properties.First(x => x.Name is "textures");
            using var doc = JsonDocument.Parse(texture.GetDecodedValue() ?? "{}");
            return new TextureProperty(doc.RootElement);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Profile texture information, containing skin and cape URLs.
    /// </summary>
    public class TextureProperty
    {
        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        /// <value>The timestamp.</value>
        public long Timestamp { get; }

        /// <summary>
        /// Gets the profile skin.
        /// </summary>
        /// <value>The profile skin.</value>
        public Skin? Skin { get; }

        /// <summary>
        /// Gets the cape URL.
        /// </summary>
        /// <value>The cape URL.</value>
        public string? CapeUrl { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="TextureProperty"/>.
        /// </summary>
        /// <param name="json">The JSON containing texture data.</param>
        internal TextureProperty(JsonElement json)
        {
            if (json.TryGetProperty("timestamp", out var time))
                Timestamp = time.GetInt64();

            if (json.TryGetProperty("textures", out var texture))
            {
                if (texture.TryGetProperty("SKIN", out var skin))
                    Skin = new Skin(skin);
                if (texture.TryGetProperty("CAPE", out var cape))
                    CapeUrl = cape.GetProperty("url").GetString();
            }
        }
    }
}

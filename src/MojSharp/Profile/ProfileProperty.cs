using System.Text;
using System.Text.Json;
using MojSharp.Common;

namespace MojSharp.Profile;

/// <summary>
/// Minecraft player profile property.
/// </summary>
public class ProfileProperty
{
    /// <summary>
    /// Gets the name of the property.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the value of the property.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Gets Yggdrasil's private key signature.
    /// </summary>
    public string? Signature { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="ProfileProperty"/>.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="value">The value of the property.</param>
    /// <param name="signature">The private key signature.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> or <paramref name="value"/> is <see langword="null"/> or whitespace.</exception>
    public ProfileProperty(string name, string value, string? signature = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(name));
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(nameof(string.IsNullOrWhiteSpace), nameof(value));

        Name = name;
        Value = value;
        Signature = signature;
    }

    /// <summary>
    /// Constructs a new instance of <see cref="ProfileProperty"/>.
    /// </summary>
    /// <param name="json">The JSON containing profile property.</param>
    /// <param name="nameKey">The key for retrieving property name.</param>
    /// <param name="valueKey">The key for retrieving property value.</param>
    /// <param name="sigKey">The key for retrieving property signature.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="json"/> is invalid.</exception>
    public ProfileProperty(JsonElement json, string nameKey = "name", string valueKey = "value", string sigKey = "signature")
    {
        Name = json.GetKeyString(nameKey);
        Value = json.GetKeyString(valueKey);
        if (json.TryGetProperty(sigKey, out var sig))
            Signature = sig.GetString();
    }

    /// <summary>
    /// Returns the base64 decoded value string.
    /// </summary>
    /// <returns>The base64 decoded value string, or <see langword="null"/> on error.</returns>
    public string? GetDecodedValue()
    {
        try
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(Value));
        }
        catch
        {
            return null;
        }
    }
}

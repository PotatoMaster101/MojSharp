using System.Collections;
using MojSharp.Common;
using MojSharp.Profile;
using Xunit;

namespace MojSharp.Test.Profile;

/// <summary>
/// Unit tests for <see cref="ProfileResponse"/>.
/// </summary>
public class ProfileResponseTest
{
    [Theory, ClassData(typeof(GetTextureReturnsTextureDataTestData))]
    public void GetTexture_Returns_TextureData(ProfileProperty texture, long expectedTime, string expectedSkinUrl, string expectedCapeUrl)
    {
        // arrange
        var profile = new ProfileResponse("foo", new Player("foo", "bar"), new[] { texture });

        // act
        var textureProp = profile.GetTextures();

        // assert
        Assert.Equal(expectedTime, textureProp?.Timestamp);
        Assert.Equal(expectedSkinUrl, textureProp?.Skin?.Url);
        Assert.Equal(expectedCapeUrl, textureProp?.CapeUrl);
    }

    [Fact]
    public void GetTextures_ReturnsNull_WhenNoTexture()
    {
        // arrange
        var profile = new ProfileResponse("foo", new Player("foo", "bar"), Array.Empty<ProfileProperty>());

        // act
        var texture = profile.GetTextures();

        // assert
        Assert.Null(texture);
    }

    /// <summary>
    /// Test data for <see cref="ProfileResponseTest.GetTexture_Returns_TextureData"/>.
    /// </summary>
    private class GetTextureReturnsTextureDataTestData : IEnumerable<object[]>
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new ProfileProperty("textures", "ewogICJ0aW1lc3RhbXAiIDogMTYzNjgwOTU4NjYxNywKICAicHJvZmlsZUlkIiA6ICI4YjU3MDc4YmYxYmQ0NWRmODNjNGQ4OGQxNjc2OGZiZSIsCiAgInByb2ZpbGVOYW1lIiA6ICJNSEZfUGlnIiwKICAidGV4dHVyZXMiIDogewogICAgIlNLSU4iIDogewogICAgICAidXJsIiA6ICJodHRwOi8vdGV4dHVyZXMubWluZWNyYWZ0Lm5ldC90ZXh0dXJlL2E1NjJhMzdiODcxZjk2NGJmYzNlMTMxMWVhNjcyYWFhMDM5ODRhNWRjNDcyMTU0YTM0ZGMyNWFmMTU3ZTM4MmIiCiAgICB9CiAgfQp9"), 1636809586617, "http://textures.minecraft.net/texture/a562a37b871f964bfc3e1311ea672aaa03984a5dc472154a34dc25af157e382b", null! };
            yield return new object[] { new ProfileProperty("textures", "ewogICJ0aW1lc3RhbXAiIDogMTYzNjgwOTc1MDA1NCwKICAicHJvZmlsZUlkIiA6ICI2MTY5OWIyZWQzMjc0YTAxOWYxZTBlYThjM2YwNmJjNiIsCiAgInByb2ZpbGVOYW1lIiA6ICJEaW5uZXJib25lIiwKICAidGV4dHVyZXMiIDogewogICAgIlNLSU4iIDogewogICAgICAidXJsIiA6ICJodHRwOi8vdGV4dHVyZXMubWluZWNyYWZ0Lm5ldC90ZXh0dXJlLzUwYzQxMGZhZDhkOWQ4ODI1YWQ1NmIwZTQ0M2UyNzc3YTZiNDZiZmEyMGRhY2QxZDJmNTVlZGM3MWZiZWIwNmQiCiAgICB9LAogICAgIkNBUEUiIDogewogICAgICAidXJsIiA6ICJodHRwOi8vdGV4dHVyZXMubWluZWNyYWZ0Lm5ldC90ZXh0dXJlLzU3ODZmZTk5YmUzNzdkZmI2ODU4ODU5ZjkyNmM0ZGJjOTk1NzUxZTkxY2VlMzczNDY4YzVmYmY0ODY1ZTcxNTEiCiAgICB9CiAgfQp9"), 1636809750054, "http://textures.minecraft.net/texture/50c410fad8d9d8825ad56b0e443e2777a6b46bfa20dacd1d2f55edc71fbeb06d", "http://textures.minecraft.net/texture/5786fe99be377dfb6858859f926c4dbc995751e91cee373468c5fbf4865e7151" };
        }
    }
}

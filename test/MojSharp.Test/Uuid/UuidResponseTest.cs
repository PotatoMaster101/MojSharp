using System;
using MojSharp.Uuid;
using Xunit;

namespace MojSharp.Test.Uuid;

/// <summary>
/// Unit tests for <see cref="UuidResponse"/>.
/// </summary>
public class UuidResponseTest
{
    [Fact]
    public void Constructor_Throws_OnInvalidArgs()
    {
        // arrange, act, assert
        Assert.Throws<ArgumentNullException>(() => new UuidResponse("foo", null!));
    }
}

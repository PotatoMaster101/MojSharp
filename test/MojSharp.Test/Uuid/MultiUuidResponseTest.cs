using System;
using MojSharp.Uuid;
using Xunit;

namespace MojSharp.Test.Uuid;

/// <summary>
/// Unit tests for <see cref="MultiUuidResponse"/>.
/// </summary>
public class MultiUuidResponseTest
{
    [Fact]
    public void Constructor_Throws_OnInvalidArgs()
    {
        // arrange, act, assert
        Assert.Throws<ArgumentNullException>(() => new MultiUuidResponse("foo", null!));
    }
}

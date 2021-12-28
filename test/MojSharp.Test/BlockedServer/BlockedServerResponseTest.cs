using System;
using MojSharp.BlockedServer;
using Xunit;

namespace MojSharp.Test.BlockedServer;

/// <summary>
/// Unit tests for <see cref="BlockedServerResponse"/>.
/// </summary>
public class BlockedServerResponseTest
{
    [Fact]
    public void Constructor_Throws_OnNullArgs()
    {
        // arrange, act, assert
        Assert.Throws<ArgumentNullException>(() => new BlockedServerResponse("foo", null!));
    }
}

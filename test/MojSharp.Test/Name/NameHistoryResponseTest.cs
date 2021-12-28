using System;
using MojSharp.Name;
using Xunit;

namespace MojSharp.Test.Name;

/// <summary>
/// Unit tests for <see cref="NameHistoryResponse"/>.
/// </summary>
public class NameHistoryResponseTest
{
    [Fact]
    public void Constructor_Throws_OnInvalidArgs()
    {
        // arrange, act, assert
        Assert.Throws<ArgumentNullException>(() => new NameHistoryResponse("foo", null!));
    }
}

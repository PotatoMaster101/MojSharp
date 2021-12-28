using System;
using System.Text.Json;
using MojSharp.Name;
using Xunit;

namespace MojSharp.Test.Name;

/// <summary>
/// Unit tests for <see cref="NameHistory"/>.
/// </summary>
public class NameHistoryTest
{
    [Theory]
    [InlineData("foo", 10)]
    [InlineData("bar", 0)]
    public void StringConstructor_Sets_Members(string username, long time)
    {
        // arrange, act
        var history = new NameHistory(username, time);

        // assert
        Assert.Equal(username, history.Username);
        Assert.Equal(time, history.Time);
        Assert.Equal(time is not 0, history.NameChanged);
    }

    [Theory]
    [InlineData(null, 10)]
    [InlineData("", 10)]
    [InlineData("   ", 10)]
    [InlineData("foo", -1)]
    public void StringConstructor_Throws_OnInvalidArgs(string username, long time)
    {
        // arrange, act, assert
        Assert.Throws<ArgumentException>(() => new NameHistory(username, time));
    }

    [Theory]
    [InlineData(@"{""name"":""foo"",""changedToAt"":10}", "name", "changedToAt", "foo", 10)]
    [InlineData(@"{""name"":""foo""}", "name", "changedToAt", "foo", 0)]
    public void JsonConstructor_Sets_Members(string json, string nameKey, string timeKey, string expectedName, long expectedTime)
    {
        // arrange
        using var doc = JsonDocument.Parse(json);

        // act
        var history = new NameHistory(doc.RootElement, nameKey, timeKey);

        // assert
        Assert.Equal(expectedName, history.Username);
        Assert.Equal(expectedTime, history.Time);
    }

    [Theory]
    [InlineData(@"{}")]
    [InlineData(@"{""name"":""foo"", ""changedToAt"":-1}")]
    [InlineData(@"{""name"":"""", ""changedToAt"":10}")]
    [InlineData(@"{""name"":""   "", ""changedToAt"":10}")]
    public void JsonConstructor_Throws_OnInvalidArgs(string json)
    {
        // arrange
        using var doc = JsonDocument.Parse(json);

        // act, assert
        Assert.Throws<ArgumentException>(() => new NameHistory(doc.RootElement));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(999999)]
    public void ToDateTime_Returns_CorrectTime(long time)
    {
        // arrange
        var history = new NameHistory("foo", time);
        var expected = DateTimeOffset.FromUnixTimeMilliseconds(time).DateTime;

        // act
        var dateTime = history.ToDateTime();

        // assert
        Assert.Equal(expected, dateTime);
    }
}

using MojSharp.Stats;

if (args.Length is 0)
{
    Console.Error.WriteLine("No stats");
    return;
}

var keys = args.Select(x => x.ToMetricKey()).ToList();
Console.WriteLine($"Request stats for: {string.Join(", ", keys.Select(x => x.ToMetricString()))}");

var stats = await new StatsRequest(keys).Request();
Console.WriteLine($"Total:    {stats.Total}");
Console.WriteLine($"Last 24h: {stats.Last24H}");
Console.WriteLine($"Average:  {stats.SalePerSecond}");

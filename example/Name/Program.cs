using MojSharp.Name;
using MojSharp.Uuid;

if (args.Length is 0)
{
    Console.Error.WriteLine("No username");
    return;
}

var player = await new UuidRequest(args[0]).Request();
var names = await new NameHistoryRequest(player.Player.Uuid).Request();
foreach (var name in names.Histories)
    Console.WriteLine($"{name.Username,-18}{(name.NameChanged ? $" at {name.ToDateTime()}" : " (original)")}");

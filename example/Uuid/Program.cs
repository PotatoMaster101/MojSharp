using MojSharp.Uuid;

switch (args.Length)
{
    case 0:
    {
        Console.Error.WriteLine("No usernames");
        break;
    }
    case 1:
    {
        var response = await new UuidRequest(args[0]).Request();
        Console.WriteLine($"{response.Player.Username,-18} : {response.Player.Uuid}");
        break;
    }
    default:
    {
        foreach (var chunk in args.Chunk(10))
        {
            var response = await new MultiUuidRequest(chunk).Request();
            foreach (var player in response.Players)
                Console.WriteLine($"{player.Username, -18} : {player.Uuid}");
        }
        break;
    }
}

using MojSharp.BlockedServer;

var blocked = await new BlockedServerRequest().Request();
Console.WriteLine(blocked.RawData);

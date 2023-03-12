using MojSharp.Authentication.Mojang;

if (args.Length < 2)
{
    Console.Error.WriteLine("No email and password");
    return;
}

var auth = await new AuthenticationRequest(args[0], args[1]).Request();
Console.WriteLine($"Client token: {auth.ClientToken}");
Console.WriteLine($"Access token: {auth.AccessToken}");
Console.WriteLine($"Username:     {auth.Profile.Username}");
Console.WriteLine($"UUID:         {auth.Profile.Uuid}");

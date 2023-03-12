using MojSharp.Profile;
using MojSharp.Uuid;

if (args.Length is 0)
{
    Console.Error.WriteLine("No username");
    return;
}

var player = await new UuidRequest(args[0]).Request();
var profile = await new ProfileRequest(player.Player.Uuid).Request();
Console.WriteLine($"Username:   {profile.Player.Username}");
Console.WriteLine($"UUID:       {profile.Player.Uuid}");

var texture = profile.GetTextures();
if (texture is null)
{
    Console.WriteLine("No texture found");
    return;
}

if (texture.Skin is null)
{
    Console.WriteLine("Skin URL: not found, default skin?");
}
else
{
    Console.WriteLine($"Skin URL:   {texture.Skin.Url}");
    Console.WriteLine($"Alex Style: {texture.Skin.Slim}");
}

Console.WriteLine(texture.CapeUrl is null ? "Cape URL:   not found, no cape?" : $"Cape URL:   {texture.CapeUrl}");

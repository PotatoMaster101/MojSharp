# Mojang Sharp
[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://github.com/PotatoMaster101/MojSharp/blob/main/LICENSE)
[![.NET](https://github.com/PotatoMaster101/MojSharp/actions/workflows/dotnet.yml/badge.svg)](https://github.com/PotatoMaster101/MojSharp/actions/workflows/dotnet.yml)
[![Nuget](https://img.shields.io/nuget/v/MojSharp)](https://www.nuget.org/packages/MojSharp)

An asynchronous [Mojang API](https://wiki.vg/Mojang_API) wrapper targeting [.NET 7](https://dotnet.microsoft.com/) and above.

## Building
Build with [`dotnet`](https://dotnet.microsoft.com/):
```
$ dotnet build -c Release
```

## Testing
Run all unit tests with [`dotnet`](https://dotnet.microsoft.com/):
```
$ dotnet test -c Release
```

## Quick Start
### Username -> UUID
**Single UUID**
```cs
var response = await new UuidRequest("PotatoMaster101").Request();
Console.WriteLine($"{response.Player.Username} : {response.Player.Uuid}");
```

**Multiple UUID**
```cs
var users = new[] { "Notch", "PotatoMaster101" };
var response = await new MultiUuidRequest(users).Request();
foreach (var player in response.Players)
    Console.WriteLine($"{player.Username} : {player.Uuid}");
```

### UUID -> Profile
```cs
var response = await new ProfileRequest("cb2671d590b84dfe9b1c73683d451d1a").Request();
var texture = response.GetTextures();
Console.WriteLine($"Skin URL: {texture?.Skin?.Url}");
Console.WriteLine($"Alex Style: {texture?.Skin?.Slim}");
Console.WriteLine($"Cape URL: {texture?.CapeUrl}");
```

### Blocked Servers
```cs
var response = await new BlockedServerRequest().Request();
Console.WriteLine(response.RawData);
```

### Mojang Authentication
```cs
var response = await new AuthenticationRequest("username", "password").Request();
Console.WriteLine($"Client token: {response.ClientToken}");
Console.WriteLine($"Access token: {response.AccessToken}");
Console.WriteLine($"Username: {response.Profile.Username}");
Console.WriteLine($"UUID: {response.Profile.Uuid}");
```

## Examples
Example projects can be found under `examples`.

## TODO
- Implement rest of API including Microsoft auth

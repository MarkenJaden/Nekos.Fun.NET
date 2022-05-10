# :tada: Welcome to `Nekos.Fun.NET` repository
[![wakatime](https://wakatime.com/badge/user/17f322c9-222a-48b4-9e15-983c41f7aed4/project/fd433962-b9fd-490b-bf3b-c517b45829b3.svg)](https://wakatime.com/badge/user/17f322c9-222a-48b4-9e15-983c41f7aed4/project/fd433962-b9fd-490b-bf3b-c517b45829b3)
[![GH_UserCount](https://badgen.net/github/dependents-repo/MarkenJaden/Nekos.Fun.NET)](https://github.com/MarkenJaden/Nekos.Fun.NET/network/dependents)
[![NG_LatestVersion](https://badgen.net/nuget/v/Nekos.Fun.NET/latest)](https://www.nuget.org/packages/Nekos.Fun.NET/)
[![NG_DLCount](https://badgen.net/nuget/dt/Nekos.Fun.NET)](https://www.nuget.org/packages/Nekos.Fun.NET/)
[![Discord_MemberCount](https://badgen.net/discord/members/ZZGTwCZprC)](https://discord.gg/ZZGTwCZprC)

`Nekos.Fun.NET` is an asynchronous library to interact with [Neko.Fun](https://nekos.fun/) API, currently
supporting v1 API. If you love this repo, consider giving it a star :star:

# :question: How to use
### Version 1
```c#
namespace Hello.There.Nekos;

public class Program
{
    public async Task ExecuteMeAsync()
    {
        NekoV1Client client = new();
        var neko = await client.RequestSfwResultsAsync(SfwEndpoint.Hug);
        var imageUrl = neko.First().Url;
    }
}
```
### With logging (+[Serilog](https://github.com/serilog/serilog))
```c#
namespace Hello.There.Nekos;

public class Program
{
    private NekosV2Client _clientWithLogging;

    public void CreateAClientWithLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();
            
        _clientWithLogging = new(new SerilogLoggerProvider(Log.Logger).CreateLogger("Nekos"));        
    }
}
```

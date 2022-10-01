using System;
using System.Threading.Tasks;
using Config.Net;
using Dcr.Config;
using Dcr.Services;
using Dcr.Utils;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;

namespace Dcr;

public class Bot
{
    public static async Task Run()
    {
        var provider = GetServices();

        InstallTessData(Environment.GetEnvironmentVariable("INSTALL_TESSDATA") ?? provider.GetRequiredService<IConfiguration>().InstallTesseractData);

        provider.GetRequiredService<LogService>().Initialize();
        await provider.GetRequiredService<CommandHandlerService>().Initialize();
        await provider.GetRequiredService<StartupService>().Initialize();
    }

    private static void InstallTessData(string installTesseractData)
    { 
        if (bool.Parse(installTesseractData))
            new DownloadTesseractTrainedData().Start();
    }

    public static IConfiguration GetConfiguration => new ConfigurationBuilder<IConfiguration>()
        .UseJsonFile("configuration.json")
        .Build();
        
    private static ILogger AddLogger()
        => new LoggerConfiguration()
            .Enrich.WithExceptionDetails()
            .WriteTo.Console()
            .WriteTo.File($"{DateTime.Now:yy-MM-dd}.txt")
            .CreateLogger();
        
    private static IServiceProvider GetServices() => new ServiceCollection()
        .AddSingleton(AddLogger())
        .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
        {
            LogLevel = LogSeverity.Error,
            MessageCacheSize = 1000
        }))
        .AddSingleton(new CommandService(new CommandServiceConfig
        {
            LogLevel = LogSeverity.Error
        }))
        .AddSingleton<StartupService>()
        .AddSingleton<CommandHandlerService>()
        .AddSingleton<LogService>()
        .AddSingleton(GetConfiguration)
        .BuildServiceProvider();
}
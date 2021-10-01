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

namespace Dcr
{
    public class Bot
    {
        public async Task Run()
        {
            var provider = GetServices();
            
            InstallTessData(provider.GetRequiredService<IConfiguration>().InstallTesseractData);
            
            provider.GetRequiredService<LogService>().Initialize();
            await provider.GetRequiredService<CommandHandlerService>().Initialize();
            await provider.GetRequiredService<StartupService>().Initialize();
        }

        public void InstallTessData(bool installTesseractData)
        {
            if (installTesseractData)
                new DownloadTesseractTrainedData().Start();
        }

        public IConfiguration GetConfiguration => new ConfigurationBuilder<IConfiguration>()
            .UseJsonConfig()
            .UseJsonFile("configuration.json")
            .Build();
        
        private IServiceProvider GetServices() => new ServiceCollection()
            .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Error,
                MessageCacheSize = 1000
            }))
            .AddSingleton(new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Error,
            }))
            .AddSingleton<StartupService>()
            .AddSingleton<CommandHandlerService>()
            .AddSingleton<LogService>()
            .AddSingleton(GetConfiguration)
            .BuildServiceProvider();
    }
}
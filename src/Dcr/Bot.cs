using System;
using System.Threading.Tasks;
using Config.Net;
using Dcr.Config;
using Dcr.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Dcr
{
    public sealed class Bot
    {
        public async Task Run()
        {
            var provider = GetServices();
            
            provider.GetRequiredService<LogService>().Initialize();
            await provider.GetRequiredService<CommandHandlerService>().Initialize();
            await provider.GetRequiredService<StartupService>().Initialize();
        }

        private IConfiguration GetConfiguration => new ConfigurationBuilder<IConfiguration>()
            .UseJsonConfig()
            .UseJsonFile("configuration.json")
            .Build();
        
        private IServiceProvider GetServices() => new ServiceCollection()
            .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                MessageCacheSize = 1000
            }))
            .AddSingleton(new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Verbose,
            }))
            .AddSingleton<StartupService>()
            .AddSingleton<CommandHandlerService>()
            .AddSingleton<LogService>()
            .AddSingleton(GetConfiguration)
            .BuildServiceProvider();
    }
}
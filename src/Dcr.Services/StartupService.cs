using System;
using System.Threading.Tasks;
using Dcr.Config;
using Discord;
using Discord.WebSocket;

namespace Dcr.Services;

public class StartupService
{
    private readonly DiscordSocketClient _client;
    private readonly IConfiguration _configuration;
        
    public StartupService(DiscordSocketClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }

    public async Task Initialize()
    {
        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_TOKEN") ?? _configuration.DiscordToken);
        await _client.StartAsync();

        await Task.Delay(-1);
    }
}
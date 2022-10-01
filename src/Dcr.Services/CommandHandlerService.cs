using System;
using System.Reflection;
using System.Threading.Tasks;
using Dcr.CommandHandler;
using Dcr.Config;
using Discord.Commands;
using Discord.WebSocket;

namespace Dcr.Services;

public class CommandHandlerService
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _provider;

    public CommandHandlerService(DiscordSocketClient client, CommandService commands, IServiceProvider provider,
        IConfiguration configuration)
    {
        _client = client;
        _commands = commands;
        _provider = provider;
        _configuration = configuration;
    }

    public async Task Initialize()
    {
        _client.MessageReceived += HandleCommands;

        await _commands.AddModulesAsync(Assembly.GetAssembly(typeof(MainCommands)),
            _provider);
    }

    //See: https://docs.stillu.cc/guides/commands/intro.html
    private async Task HandleCommands(SocketMessage arg)
    {
        if (arg is not SocketUserMessage message) return;

        var argPos = 0;

        if (!(message.HasCharPrefix(
                  Convert.ToChar(Environment.GetEnvironmentVariable("PREFIX") ?? _configuration.Prefix), ref argPos) ||
              message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
            message.Author.IsBot)
            return;

        var context = new SocketCommandContext(_client, message);

        await _commands.ExecuteAsync(
            context,
            argPos,
            _provider);
    }
}
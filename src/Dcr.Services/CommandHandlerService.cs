using System;
using System.Reflection;
using System.Threading.Tasks;
using Dcr.Config;
using Discord.Commands;
using Discord.WebSocket;
using Dcr.CommandHandler;

namespace Dcr.Services
{
    public class CommandHandlerService
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _provider;
        private readonly IConfiguration _configuration;
        
        public CommandHandlerService(DiscordSocketClient client, CommandService commands, IServiceProvider provider, IConfiguration configuration)
        {
            _client = client;
            _commands = commands;
            _provider = provider;
            _configuration = configuration;
        }

        public async Task Initialize()
        {
            _client.MessageReceived += HandleCommands;
            
            await _commands.AddModulesAsync(assembly: Assembly.GetAssembly(typeof(MainCommands)), 
                services: _provider);
        }

        //See: https://docs.stillu.cc/guides/commands/intro.html
        private async Task HandleCommands(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;
            
            if (!(message.HasCharPrefix(Convert.ToChar(Environment.GetEnvironmentVariable("PREFIX") ?? _configuration.Prefix), ref argPos) || 
                  message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;
            
            var context = new SocketCommandContext(_client, message);
            
            await _commands.ExecuteAsync(
                context: context, 
                argPos: argPos,
                services: _provider);
        }
    }
}
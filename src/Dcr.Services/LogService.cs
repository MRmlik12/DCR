using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Serilog;
using Serilog.Exceptions;

namespace Dcr.Services
{
    public class LogService
    {
        private readonly DiscordSocketClient _client;
        
        public LogService(DiscordSocketClient client)
        {
            _client = client;
        }

        public void Initialize()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
                .WriteTo.File($"{DateTime.Now:yy-MM-dd}.txt")
                .CreateLogger();
            
            _client.Log += LogMesage;
        }

        private Task LogMesage(LogMessage arg)
        {
            Log.Information(arg.Message);
            return Task.CompletedTask;
        }
    }
}

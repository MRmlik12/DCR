using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Serilog;

namespace Dcr.Services;

public class LogService
{
    private readonly DiscordSocketClient _client;
    private readonly ILogger _logger;

    public LogService(DiscordSocketClient client, ILogger logger)
    {
        _client = client;
        _logger = logger;
    }

    public void Initialize()
    {
        _client.Log += LogMessage;
    }

    private Task LogMessage(LogMessage arg)
    {
        _logger.Information(arg.Message);
        return Task.CompletedTask;
    }
}
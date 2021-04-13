using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dcr.Utils;
using Discord.Commands;
using Discord.WebSocket;

namespace Dcr.CommandHandler
{
    public class MainCommands : ModuleBase<SocketCommandContext>
    {
        private readonly DiscordSocketClient _client;
        private readonly Ocr _ocr;
        private readonly WebClient _webClient;

        public MainCommands(DiscordSocketClient client)
        {
            _client = client;
            _ocr = new Ocr();
            _webClient = new WebClient();
        }
        
        [Command("ping")]
        [Description("Checks bot response time!")]
        public async Task Ping()
            => await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} " +
                                                      $" :ping_pong: Pong in {_client.Latency.ToString()}ms!");

        [Command("read")]
        [Description("Reads text from image and returns all colleted data")]
        public async Task Read()
        {
            if (Context.Message.Attachments.Count == 0)
            {
                await Context.Channel.SendMessageAsync(
                    $"{Context.Message.Author.Mention} This message isn't contain image!");
                return;
            }
            
            var downloadedData = _webClient.DownloadData(Context.Message.Attachments.ElementAt(0).Url);
            var text = _ocr.GetText(downloadedData);
            await Context.Channel.SendMessageAsync($"```{text}```");
        }
    }
}
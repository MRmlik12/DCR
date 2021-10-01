using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dcr.Utils;
using Discord;
using Discord.Commands;

namespace Dcr.CommandHandler
{
    public class MainCommands : ModuleBase<SocketCommandContext>
    {
        private readonly Ocr _ocr;
        private readonly WebClient _webClient;
        private const string GithubUrl = "https://github.com/MRmlik12/DCR";

        public MainCommands()
        {
            _ocr = new Ocr();
            _webClient = new WebClient();
        }
        
        [Command("ping")]
        [Description("Checks bot response time!")]
        public async Task Ping()
            => await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} " +
                                                      $" :ping_pong: Pong in {Context.Client.Latency.ToString()}ms!");

        [Command("read")]
        [Description("Reads text from image and returns all colleted data")]
        public async Task Read([Remainder] string url = null)
        {
            if (!string.IsNullOrEmpty(url))
            {
                await Context.Channel.SendMessageAsync($"```{GetTextFromImage(url)}```");
                return;
            }
            
            if (Context.Message.Attachments.Count == 0)
            {
                await Context.Channel.SendMessageAsync(
                    $"{Context.Message.Author.Mention} This message isn't contain image!");
                return;
            }
            
            var text = GetTextFromImage(Context.Message.Attachments.ElementAt(0).Url).Result;
            await Context.Channel.SendMessageAsync($"```{text}```");
        }

        [Command("help")]
        public async Task Help()
            => await Context.Channel.SendMessageAsync($"{GithubUrl}/blob/main/README.md");

        [Command("about")]
        public async Task About()
        {
            var builder = new EmbedBuilder()
                .WithTitle("About")
                .AddField("Version", typeof(Ocr).Assembly.GetName().Version)
                .AddField("Author", "MRmlik12")
                .AddField("Github", GithubUrl)
                .WithFooter($"Requested Date: {DateTime.UtcNow}")
                .Build();
            await Context.Channel.SendMessageAsync(embed: builder);
        }

        private async Task<string> GetTextFromImage(string imageUrl)
        {
            var downloadedData = await _webClient.DownloadDataTaskAsync(imageUrl);
            var text = _ocr.GetText(downloadedData);
            return text;
        }
    }
}
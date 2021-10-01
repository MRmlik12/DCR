using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dcr.CommandHandler.Arguments;
using Dcr.Utils;
using Discord;
using Discord.Commands;

namespace Dcr.CommandHandler
{
    public class MainCommands : ModuleBase<SocketCommandContext>
    {
        private readonly Ocr _ocr;
        private readonly WebClient _webClient;
        private readonly string _tessdataPath;
        private const string GithubUrl = "https://github.com/MRmlik12/DCR";

        public MainCommands()
        {
            _ocr = new Ocr();
            _webClient = new WebClient();
            _tessdataPath = GetTesseractDataPath();
        }
        
        [Command("ping")]
        [Description("Checks bot response time!")]
        public async Task Ping()
            => await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} " +
                                                      $" :ping_pong: Pong in {Context.Client.Latency.ToString()}ms!");

        [Command("read")]
        [Description("Reads text from image and returns all colleted data")]
        public async Task Read([Remainder]ReadArguments readArguments)
        {
            if (!string.IsNullOrEmpty(readArguments.Url))
            {
                await Context.Channel.SendMessageAsync($"```{GetTextFromImage(readArguments.Url, readArguments.Lang)}```");
                return;
            }
            
            if (Context.Message.Attachments.Count == 0)
            {
                await Context.Channel.SendMessageAsync(
                    $"{Context.Message.Author.Mention} This message isn't contain image!");
                return;
            }
            
            var text = GetTextFromImage(Context.Message.Attachments.ElementAt(0).Url, readArguments.Lang).Result;
            await Context.Channel.SendMessageAsync($"```{text}```");
        }
        
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

        private async Task<string> GetTextFromImage(string imageUrl, string lang = "eng")
        {
            var downloadedData = await _webClient.DownloadDataTaskAsync(imageUrl);
            var text = _ocr.GetText(downloadedData, lang, _tessdataPath);
            return text;
        }

        private string GetTesseractDataPath()
            => Directory.Exists("tessdata-extended") ? "tessdata-extended" : "tessdata";
    }
}
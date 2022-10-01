using Discord.Commands;

namespace Dcr.CommandHandler.Arguments;

[NamedArgumentType]
public class ReadArguments
{
    public string Lang { get; set; }
    public string Url { get; set; }
    public string File { get; set; }
}
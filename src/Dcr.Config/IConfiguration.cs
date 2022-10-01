namespace Dcr.Config;

public interface IConfiguration
{
    string DiscordToken { get; set; }
    string InstallTesseractData { get; set; }
    string Prefix { get; set; }
}
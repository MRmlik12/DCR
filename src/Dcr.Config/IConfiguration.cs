namespace Dcr.Config
{
    public interface IConfiguration
    {
        string DiscordToken { get; set; }
        bool InstallTesseractData { get; set; }
        string Prefix { get; set; }
    }
}
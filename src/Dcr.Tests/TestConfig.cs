using Xunit;

namespace Dcr.Tests
{
    public class TestConfig
    {
        private const string DiscordTokenResult = "TOKEN";
        private const string PrefixExpected = "!";
        
        [Fact]
        public void Is_Properly_Parsing_Config()
        {
            var configuration = new Bot().GetConfiguration;
            Assert.Equal(DiscordTokenResult, configuration.DiscordToken);
            Assert.Equal(PrefixExpected, configuration.Prefix);
        }
    }
}
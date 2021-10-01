using System.IO;
using Dcr.Utils;
using Xunit;

namespace Dcr.Tests
{
    public class TestDownloadTesseractTrainedData
    {
        [Fact]
        public void TryExtractZipFileAndExtractRenameToTessData()
        {
            var tessData = new DownloadTesseractTrainedData();
            tessData.Start();
            
            Assert.True(Directory.Exists("tessdata"));
            Assert.False(File.Exists("temp.zip"));
        }
    }
}
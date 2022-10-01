using System.IO;
using Dcr.Utils;

namespace Dcr.Tests;

public class TestDownloadTesseractTrainedData
{
    private const string PathToTempFile = "./AssetsToTest/temp.zip";

    [Fact]
    public void TestExtractZipFileAndExtractRenameToTessData_CheckIfTempFileRemovedAndTessdataExtendedExists()
    {
        var tessData = new DownloadTesseractTrainedData(PathToTempFile);
        tessData.Start();

        Assert.True(Directory.Exists("tessdata-extended"));
        Assert.False(File.Exists("temp.zip"));
    }
}
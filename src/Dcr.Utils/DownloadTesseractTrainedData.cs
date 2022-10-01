using System.IO;
using System.Net;
using Ionic.Zip;
using Serilog;

namespace Dcr.Utils;

public class DownloadTesseractTrainedData
{
    private const string TessDataUrl = "https://github.com/tesseract-ocr/tessdata/archive/refs/heads/main.zip";
    private readonly string _fileName;

    public DownloadTesseractTrainedData(string tempPath = "temp.zip")
    {
        _fileName = tempPath;
    }

    public void Start()
    {
        Log.Information("Start installing tessdata");

        if (CheckIfTessDataDirExists())
            return;

        if (CheckIfTempExists())
        {
            ExtractData();
            RenameToTessdata();
            DeleteTempFile();
            return;
        }

        DownloadFile();
        ExtractData();
        RenameToTessdata();
        DeleteTempFile();

        Log.Information("Tessdata was installed successfully");
    }

    private bool CheckIfTempExists()
    {
        return File.Exists(_fileName);
    }

    private static bool CheckIfTessDataDirExists()
    {
        return Directory.Exists("tessdata-extended");
    }

    private static void RenameToTessdata()
    {
        Directory.Move("tessdata-main", "tessdata-extended");
    }

    private void DownloadFile()
    {
        Log.Information("Downloading tessdata...");
#pragma warning disable SYSLIB0014
        var webClient = new WebClient();
#pragma warning restore SYSLIB0014
        webClient.DownloadFile(TessDataUrl, _fileName);
    }

    private void DeleteTempFile()
    {
        File.Delete(_fileName);
    }

    private void ExtractData()
    {
        Log.Information("Extracting tessdata");

        using var zip = ZipFile.Read(_fileName);
        zip.ExtractAll("./");
    }
}
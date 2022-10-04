using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        DownloadFile().GetAwaiter().GetResult();
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

    private async Task DownloadFile()
    {
        Log.Information("Downloading tessdata...");

        using var client = new HttpClient();
        client.Timeout = TimeSpan.FromMinutes(60.0);
        var content = await client.GetAsync(TessDataUrl);
        var file = File.Create(_fileName);
        file.Close();
        await File.WriteAllBytesAsync(_fileName, await content.Content.ReadAsByteArrayAsync());
        await ExtractData();
    }

    private void DeleteTempFile()
    {
        File.Delete(_fileName);
    }

    private async Task ExtractData()
    {
        Log.Information("Extracting tessdata");

        using var zip = ZipFile.Read(_fileName);
        zip.ExtractAll("./");
    }
}
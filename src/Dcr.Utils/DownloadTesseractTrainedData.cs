using System.IO;
using System.Net;
using Ionic.Zip;
using Serilog;

namespace Dcr.Utils
{
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
            => File.Exists(_fileName);

        private bool CheckIfTessDataDirExists()
            => Directory.Exists("tessdata-extended");

        private void RenameToTessdata()
            => Directory.Move("tessdata-main", "tessdata-extended");

        private void DownloadFile()
        {
            Log.Information("Downloading tessdata...");
            var webClient = new WebClient();
            webClient.DownloadFile(TessDataUrl, _fileName);
        }

        private void DeleteTempFile()
            => File.Delete(_fileName);

        private void ExtractData()
        {
            Log.Information("Extracting tessdata");
            
            using ZipFile zip = ZipFile.Read(_fileName);
            zip.ExtractAll("./");
        }
    }
}
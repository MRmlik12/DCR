using System.IO;
using System.Net;
using Ionic.Zip;
using Serilog;

namespace Dcr.Utils
{
    public class DownloadTesseractTrainedData
    {
        private const string TessDataUrl = "https://github.com/tesseract-ocr/tessdata/archive/refs/heads/main.zip";
        private const string FileName = "temp.zip";

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
            }
            
            DownloadFile();
            ExtractData();
            RenameToTessdata();
            DeleteTempFile();
            
            Log.Information("Tessdata was installed successfully");
        }

        private bool CheckIfTempExists() 
            => File.Exists(FileName);

        private bool CheckIfTessDataDirExists()
            => Directory.Exists(TessDataUrl);

        private void RenameToTessdata()
            => Directory.Move("tessdata-main", "tessdata");

        private void DownloadFile()
        {
            Log.Information("Downloading tessdata...");
            var webClient = new WebClient();
            webClient.DownloadFile(TessDataUrl, FileName);
        }

        private void DeleteTempFile()
            => File.Delete(FileName);

        private void ExtractData()
        {
            Log.Information("Extracting tessdata");
            
            using ZipFile zip = ZipFile.Read(FileName);
            zip.ExtractAll("./");
        }
    }
}
using System;
using Tesseract;

namespace Dcr.Utils
{
    public class Ocr
    {
        public string GetText(byte[] imageData, string lang, string tessdataPath = "tessdata")
        {
            using var engine = new TesseractEngine(tessdataPath, lang, EngineMode.Default);
            using var img = Pix.LoadFromMemory(imageData);
            using var page = engine.Process(img);
            
            return page.GetText();
        }
    }
}
using System;
using Tesseract;

namespace Dcr.Utils
{
    public class Ocr
    {
        public string GetText(byte[] imageData)
        {
            using var engine = new TesseractEngine("tessdata", "eng", EngineMode.Default);
            using var img = Pix.LoadFromMemory(imageData);
            using var page = engine.Process(img);
            
            return page.GetText();
        }
    }
}
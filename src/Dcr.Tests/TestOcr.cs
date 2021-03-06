using System;
using System.IO;
using Dcr.Utils;
using Xunit;

namespace Dcr.Tests
{
    public class TestOcr
    {
        private const string ExpectedText = "How to read a posted file in C# Discord.net?\n";
        
        [Fact]
        public void Check_image_text()
        {
            var ocr = new Ocr();
            var imageBytes = File.ReadAllBytes("AssetsToTest/texttoread.png");
            var text = ocr.GetText(imageBytes, "eng");
            Assert.Equal(ExpectedText, text);
        }
    }
}
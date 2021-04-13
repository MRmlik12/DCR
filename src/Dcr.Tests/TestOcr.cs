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
            var imageBytes = File.ReadAllBytes("Images/texttoread.png");
            string text = ocr.GetText(imageBytes);
            Assert.Equal(ExpectedText, text);
        }
    }
}
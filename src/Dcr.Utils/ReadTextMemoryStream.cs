using System.IO;
using System.Text;

namespace Dcr.Utils
{
    public static class ReadTextMemoryStream
    {
        public static MemoryStream GetReadTextMemoryStream(string readedText)
        {
            using var memoryStream = new MemoryStream(2048);
            memoryStream.Write(Encoding.UTF8.GetBytes(readedText), 0, readedText.Length);

            return memoryStream;
        }
    }
}
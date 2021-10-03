using System.IO;
using System.Text;

namespace Dcr.Utils
{
    public static class ReadTextMemoryStream
    {
        public static MemoryStream GetReadTextMemoryStream(string readText)
            => new MemoryStream(Encoding.ASCII.GetBytes(readText));
    }
}
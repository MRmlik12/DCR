using System.IO;
using Dcr.Utils;

namespace Dcr.Tests;

public class TestReadTextMemoryStream
{
    private const string TextToStream = "LOREM IPSUM, LOREM IPSUM";
        
    [Fact]
    public void TestReadMemoryStream_CheckTextFromStreamIsValid()
    {
        var stream = ReadTextMemoryStream.GetReadTextMemoryStream(TextToStream);
        var streamReader = new StreamReader(stream);
        var result = streamReader.ReadToEnd();
            
        streamReader.Dispose();
        stream.Dispose();
            
        Assert.Equal(TextToStream, result);
    }
}
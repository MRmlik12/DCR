using Dcr.Utils;

namespace Dcr.Tests;

public class TestTessDataLanguages
{
    private readonly TessDataLanguages _tessDataLanguages;

    public TestTessDataLanguages()
    {
        _tessDataLanguages = new TessDataLanguages();
    }

    [Fact]
    public async void TestTessDataLanguage_ChecksIfElementNotEmpty()
    {
        var result = await _tessDataLanguages.GetTessDataLanguages();

        Assert.NotEmpty(result);
    }
}
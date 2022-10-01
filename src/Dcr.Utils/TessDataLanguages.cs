using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Dcr.Utils.Pocos;

namespace Dcr.Utils;

public class TessDataLanguages
{
    private const string WebsiteUrl = "https://tesseract-ocr.github.io/tessdoc/Data-Files";
    private IDocument _document;
    private readonly List<TesseractDataLanguage> _tesseractDataLanguages;

    public TessDataLanguages()
    {
        _tesseractDataLanguages = new List<TesseractDataLanguage>();
    }
        
    public async Task<List<TesseractDataLanguage>> GetTessDataLanguages()
    {
        await Initialize();
        ScrapContent();
        return _tesseractDataLanguages;
    }

    private async Task Initialize()
    {
        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);
        _document = await context.OpenAsync(WebsiteUrl);
    }
        
    private void ScrapContent()
    {
        var timetable = (IHtmlTableElement)_document.GetElementsByTagName("table")[2];
        foreach (var row in timetable.Rows)
        {
            if (row.Index.Equals(0))
                continue;
                
            _tesseractDataLanguages.Add(new TesseractDataLanguage
            { 
                LangCode = row.Cells[0].TextContent,
                Lang = row.Cells[1].TextContent
            });
        }
    }
}
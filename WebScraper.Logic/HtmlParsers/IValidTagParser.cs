namespace WebScraper.Logic.HtmlParsers
{
    public interface IValidTagParser
    {
        bool TryParse(string tagContents, out IHtmlTag tag);
    }
}
namespace WebScraper.Logic.HtmlParsers
{
    public interface ITagFactory
    {
        ClosingTag CreateClosingTagFromContents(string tagContents);
        OpeningTag CreateOpeningTagFromClosingTag(ClosingTag closingTag);
        OpeningTag CreateOpeningTagFromContents(string tagContents);
    }
}
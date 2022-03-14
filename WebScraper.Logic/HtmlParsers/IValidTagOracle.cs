namespace WebScraper.Logic.HtmlParsers
{
    public interface IValidTagOracle
    {
        bool TryGetNextValidTag(int currentPosition, string html, out string tagContents, out bool isOpening, out int nextPosition);
    }
}
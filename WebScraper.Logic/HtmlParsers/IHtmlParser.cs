using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public interface IHtmlParser
    {
        IReadOnlyList<IHtmlNode> ParseHtml(string html);
    }
}
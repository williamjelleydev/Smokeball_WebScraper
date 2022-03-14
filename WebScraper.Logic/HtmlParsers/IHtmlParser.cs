using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public interface IHtmlParser
    {
        IList<IHtmlNode> ParseHtml(string html);
    }
}
using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public interface IHtmlParser
    {
        IList<HtmlNode> ParseHtml(string html);
    }
}
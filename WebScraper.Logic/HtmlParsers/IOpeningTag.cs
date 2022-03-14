using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public interface IOpeningTag
    {
        string Attributes { get; }
        List<IHtmlNode> Children { get; }
        string Name { get; }
    }
}
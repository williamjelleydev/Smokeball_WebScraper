using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public interface IOpeningTag
    {
        string Attributes { get; }
        IList<IHtmlNode> Children { get; }
        string Name { get; }
    }
}
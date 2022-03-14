using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public interface IHtmlNode
    {
        IReadOnlyList<IHtmlNode> Children { get; }
        string Name { get; }

        IEnumerable<IHtmlNode> GetNodesWithAnyOfClasses(IEnumerable<string> classes);
        bool HasClass(string className);
        bool HasHrefWithUrl(string url);
    }
}
using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public interface IHtmlNode
    {
        IReadOnlyList<IHtmlNode> Children { get; }
        string Name { get; }

        IEnumerable<IHtmlNode> GetNodesWithAnyOfClasses(IEnumerable<string> classes);
        IEnumerable<IHtmlNode> GetNodesWithClass(string className);
        bool HasClass(string className);
        bool HasHrefWithUrl(string url);
    }
}
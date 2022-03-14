using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public interface IHtmlNode
    {
        IList<IHtmlNode> Children { get; }
        string Name { get; }

        IList<IHtmlNode> GetNodesWithAnyOfClasses(IEnumerable<string> classes);
        IList<IHtmlNode> GetNodesWithClass(string className);
        bool HasClass(string className);
        bool HasHrefWithUrl(string url);
    }
}
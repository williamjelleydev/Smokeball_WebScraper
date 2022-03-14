using System.Collections.Generic;
using System.Linq;

namespace WebScraper.Logic.HtmlParsers
{
    public class HtmlNode : IHtmlNode
    {
        public HtmlNode(IHtmlTag openingTag)
        {
            Name = openingTag.Name;
            Children = openingTag.Children;
            Attributes = openingTag.Attributes;
        }

        public string Name { get; }

        public IReadOnlyList<IHtmlNode> Children { get; }

        private string Attributes { get; }

        public bool HasClass(string className)
        {
            return Attributes.Contains(className);
        }

        public IEnumerable<IHtmlNode> GetNodesWithAnyOfClasses(IEnumerable<string> classes)
        {
            // TODO: Replace with more robust check once better HtmlParser is used for attributes
            var nodes = new List<IHtmlNode>();
            if (classes.Any(c => HasClass(c)))
            {
                nodes.Add(this);
            }

            foreach (var node in Children)
            {
                nodes.AddRange(node.GetNodesWithAnyOfClasses(classes));
            }

            return nodes;
        }

        public bool HasHrefWithUrl(string url)
        {
            // TODO: Replace with more robust check once better HtmlParser is used for attributes
            return Attributes.Contains(url);
        }
    }
}

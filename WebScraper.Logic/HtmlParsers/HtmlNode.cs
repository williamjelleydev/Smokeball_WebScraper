using System.Collections.Generic;
using System.Linq;

namespace WebScraper.Logic.HtmlParsers
{
    public class HtmlNode : IHtmlNode
    {
        public HtmlNode(IOpeningTag openingTag)
        {
            Name = openingTag.Name;
            Children = openingTag.Children;
            Attributes = openingTag.Attributes;
        }

        public string Name { get; }
        public IReadOnlyList<IHtmlNode> Children { get; } // TODO: make readonly, etc..

        private string Attributes { get; }


        public bool HasClass(string className)
        {
            return Attributes.Contains(className);
        }

        public IEnumerable<IHtmlNode> GetNodesWithClass(string className)
        {
            var nodes = new List<IHtmlNode>();
            if (HasClass(className))
            {
                nodes.Add(this);
            }

            foreach (var node in Children)
            {
                nodes.AddRange(node.GetNodesWithClass(className));
            }

            return nodes;
        }

        public IEnumerable<IHtmlNode> GetNodesWithAnyOfClasses(IEnumerable<string> classes)
        {
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
            return Attributes.Contains(url);
        }
    }
}

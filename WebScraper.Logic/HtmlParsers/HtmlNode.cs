using System.Collections.Generic;
using System.Linq;

namespace WebScraper.Logic.HtmlParsers
{
    public class HtmlNode
    {
        public HtmlNode(OpeningTag openingTag)
        {
            Name = openingTag.Name;
            Children = openingTag.Children;
            Attributes = openingTag.Attributes;
        }

        public string Name { get; }
        public IList<HtmlNode> Children { get; } // TODO: make readonly, etc..

        private string Attributes { get; } // TODO: stop exposing this publicly until it actually works properly..


        public bool HasClass(string className)
        {
            // TODO: re-implement this once we have proper AttributeParser lol
            return Attributes.Contains(className);
        }

        public IList<HtmlNode> GetNodesWithClass(string className)
        {
            var nodes = new List<HtmlNode>();
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

        public IList<HtmlNode> GetNodesWithAnyOfClasses(IEnumerable<string> classes)
        {
            var nodes = new List<HtmlNode>();
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

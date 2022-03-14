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

        private IList<string> Attributes { get; } // TODO: stop exposing this publicly until it actually works properly..



        //public IEnumerable<string> GetThisLevelAttributesThatMatch(string attributeName)
        //{
        //    return Attributes.Where(a => a.Contains(attributeName));
        //}

        //public IList<string> GetNestedAttributesThatMatch(string attributeName)
        //{
        //    // ew gross i have ToList()'s like this...s
        //    var thisLevelRes = GetThisLevelAttributesThatMatch(attributeName).ToList();
        //    foreach (var childNode in Children)
        //    {
        //        thisLevelRes.AddRange(childNode.GetNestedAttributesThatMatch(attributeName));
        //    }

        //    return thisLevelRes;
        //}

        //public IList<HtmlNode> GetNodesWithAttributesThatMatch(string attributeName)
        //{
        //    var res = new List<HtmlNode>();
        //    if (GetThisLevelAttributesThatMatch(attributeName).Any())
        //    {
        //        res.Add(this);
        //    }

        //    foreach (var node in Children)
        //    {
        //        res.AddRange(node.GetNodesWithAttributesThatMatch(attributeName));
        //    }

        //    return res;
        //}

        public bool HasClass(string className)
        {
            // TODO: re-implement this once we have proper AttributeParser lol
            return Attributes.Any(a => a.Contains(className));
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
            return Attributes.Any(a => a.Contains(url));
        }
    }
}

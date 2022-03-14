using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public class HtmlTag : IHtmlTag
    {
        public HtmlTag(string name, string attributes)
        {
            Name = name;
            Attributes = attributes;
        }

        public string Name { get; }

        public List<IHtmlNode> Children { get; } = new List<IHtmlNode>();

        public string Attributes { get; }
    }
}

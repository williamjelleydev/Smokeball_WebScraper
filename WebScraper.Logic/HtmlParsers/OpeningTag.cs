using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Logic.HtmlParsers
{
    public class OpeningTag
    {
        public OpeningTag(string name, IList<string> attributes)
        {
            Name = name;
            Attributes = attributes;
        }

        public string Name { get; }

        public IList<HtmlNode> Children { get; } = new List<HtmlNode>();

        public IList<string> Attributes { get; } // TODO: Have as Key-Value pairs once we implement parsing in TagFactory

    }
}

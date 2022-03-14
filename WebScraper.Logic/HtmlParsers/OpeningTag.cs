using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Logic.HtmlParsers
{
    public class OpeningTag
    {
        public OpeningTag(string name, string attributes)
        {
            Name = name;
            Attributes = attributes;
        }

        public string Name { get; }

        public IList<HtmlNode> Children { get; } = new List<HtmlNode>();

        public string Attributes { get; } // TODO: Have as Key-Value pairs once we implement parsing in TagFactory

    }
}

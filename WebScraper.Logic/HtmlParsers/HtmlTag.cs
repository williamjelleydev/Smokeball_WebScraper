using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public class HtmlTag : IHtmlTag
    {
        // just to get builds working for now..
        //public OpeningTag ToOpeningTag()
        //{
        //    return new OpeningTag(Name, Attributes);
        //}

        //public ClosingTag ToClosingTag()
        //{
        //    return new ClosingTag(Name); // okay just name for now I guess...??
        //}

        // Just hacking as a duplicat of OpenningTag for now, so I can switch these out later..s
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

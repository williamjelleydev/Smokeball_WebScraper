using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public interface IHtmlTag
    {
        string Attributes { get; }
        List<IHtmlNode> Children { get; }
        string Name { get; }

        //ClosingTag ToClosingTag();
        //OpeningTag ToOpeningTag();
    }
}
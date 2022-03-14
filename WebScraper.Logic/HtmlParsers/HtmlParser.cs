using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public class HtmlParser : IHtmlParser
    {
        private readonly ITagFactory _tagFactory;

        public HtmlParser(ITagFactory tagFactory)
        {
            _tagFactory = tagFactory;
        }

        public IList<HtmlNode> ParseHtml(string html)
        {
            var htmlNodeBuilder = new HtmlNodeBuilder(_tagFactory);
            var validTagOracle = new ValidTagOracle(); // TODO: make DI

            var currentPosition = 0;

            // I mean at this point, how useful even _is_ this validTagOracle?? lol
            while (validTagOracle.TryGetNextValidTag(currentPosition, html, out string tagContents, out bool isOpeningTag, out int nextPosition))
            {
                currentPosition = nextPosition; // TODO: confirm if this is even necessary! Does currentPosition just get updated in validTagOracle??
                if (isOpeningTag)
                {
                    var openingTag = _tagFactory.CreateOpeningTagFromContents(tagContents);
                    htmlNodeBuilder.AddOpeningTag(openingTag);
                }
                else
                {
                    var closingTag = _tagFactory.CreateClosingTagFromContents(tagContents);
                    htmlNodeBuilder.AddClosingTag(closingTag);
                }
            }

            return htmlNodeBuilder.ToHtmlNodes();
        }
    }
}

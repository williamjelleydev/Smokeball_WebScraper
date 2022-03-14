using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{/// <summary>
/// Takes input html string, and returns list of HtmlNodes, flattening only to <div> and <a>
/// tags and their attributes. This ignore everything else in html
/// </summary>
    public class DivAndAnchorFlattenedHtmlParser : IHtmlParser
    {
        private readonly ITagFactory _tagFactory;
        private readonly IValidTagOracle _validTagOracle;

        public DivAndAnchorFlattenedHtmlParser(
            ITagFactory tagFactory,
            IValidTagOracle validTagOracle
            )
        {
            _tagFactory = tagFactory;
            _validTagOracle = validTagOracle;
        }

        public IList<IHtmlNode> ParseHtml(string html)
        {
            var htmlNodeBuilder = new HtmlNodeBuilder(_tagFactory); // TODO: probably create an HtmlNodeBuilderFactory - the need for this might become more obvious when i come to unit testing this..

            var currentPosition = 0;

            // I mean at this point, how useful even _is_ this validTagOracle?? lol
            while (_validTagOracle.TryGetNextValidTag(currentPosition, html, out string tagContents, out bool isOpeningTag, out int nextPosition))
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

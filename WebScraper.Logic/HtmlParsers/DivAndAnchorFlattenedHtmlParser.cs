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

        public IReadOnlyList<IHtmlNode> ParseHtml(string html)
        {
            var htmlNodeBuilder = new HtmlNodeBuilder(_tagFactory);

            var currentPosition = 0;

            while (_validTagOracle.TryGetNextValidTag(currentPosition, html, out string tagContents, out bool isOpeningTag, out int nextPosition))
            {
                currentPosition = nextPosition;
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

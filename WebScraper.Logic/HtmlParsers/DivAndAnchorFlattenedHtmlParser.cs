using System;
using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{/// <summary>
/// Takes input html string, and returns list of HtmlNodes, flattening only to <div> and <a>
/// tags and their attributes. This ignore everything else in html
/// </summary>
    public class DivAndAnchorFlattenedHtmlParser : IHtmlParser
    {
        private readonly IValidTagParser _validTagParser;

        public DivAndAnchorFlattenedHtmlParser(IValidTagParser validTagParser)
        {
            _validTagParser = validTagParser;
        }
        public IReadOnlyList<IHtmlNode> ParseHtml(string html)
        {
            var htmlNodeBuilder = new HtmlNodeBuilder();

            var currentPosition = 0;
            var htmlLength = html.Length;

            while (currentPosition < html.Length)
            {
                while (currentPosition < html.Length && html[currentPosition] != '<')
                {
                    currentPosition++; // find next opening tag '<'
                }

                if (currentPosition >= html.Length)
                {
                    // TODO: test if this would ever get reached..
                    break;
                }
                // Ignore all characters until we find next opening tag

                // What if I just then went and found either EOF or next closing tag? Not the most efficient but if it makes for cleaner code then I'm in..
                var nextClosingPosition = currentPosition + 1;
                while (nextClosingPosition < html.Length && html[nextClosingPosition] != '>')
                {
                    nextClosingPosition++; // find next closing tag wrt current openingTag
                }

                // At this point either have closing tag '>', or at eof and nextClosingPosition == currentPosition.Length.
                currentPosition++; // move to next position

                bool isOpeningTag = true;
                if (html[currentPosition] == '/')
                {
                    isOpeningTag = false;
                    currentPosition++; // move past closing tag identifier if present
                }

                // At this point we should be on first item inside tag, the tag name.

                var tagContents = html.Substring(currentPosition, nextClosingPosition - currentPosition);
                if (_validTagParser.TryParse(tagContents, out HtmlTag tag)) {
                    if (isOpeningTag)
                    {
                        //var openingTag = tag.ToOpeningTag();
                        htmlNodeBuilder.AddOpeningTag(tag);
                    } else
                    {
                        //var closingTag = tag.ToClosingTag();
                        htmlNodeBuilder.AddClosingTag(tag);
                    }
                    currentPosition = nextClosingPosition + 1; // carry on looking for more tags after end of this one
                } else
                {
                    // Could not parse a valid tag, so continue discarding chars until next '<'
                    var stop = true;
                }

            }

            return htmlNodeBuilder.ToHtmlNodes();
        }
    }
}

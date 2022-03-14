using System;
using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{/// <summary>
/// Takes input html string, and returns list of HtmlNodes, flattening only to <div> and <a>
/// tags and their attributes. This ignores everything else in html and definitely has it's limitations.
/// For a more robust HtmlParser consider something like HtmlAgilityPack
/// </summary>
    public class DivAndAnchorFlattenedHtmlParser : IHtmlParser
    {
        private readonly IValidTagParser _validTagParser;

        public DivAndAnchorFlattenedHtmlParser(IValidTagParser validTagParser)
        {
            _validTagParser = validTagParser;
        }

        /// <summary>
        /// Ignores all chars until we find next '<'. Next Valid Tag candidate is all
        /// tagContents between this an next closing '>'. Leave it to IValidTagParser to 
        /// determine if a valid IHtmlTag can be parsed. Valid Opening/Closing tags are added 
        /// to HtmlNodeBuilder to build Node tree structure of valid div and a tags with their attributes
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public IReadOnlyList<IHtmlNode> ParseHtml(string html)
        {
            var htmlNodeBuilder = new HtmlNodeBuilder(); // TODO: ctor inject factory so we can unit test this
            var currentPosition = 0;

            while (currentPosition < html.Length)
            {
                while (currentPosition < html.Length && html[currentPosition] != '<')
                {
                    currentPosition++;
                }

                if (currentPosition >= html.Length)
                {
                    break; // end of html
                }

                var nextClosingPosition = currentPosition + 1;
                while (nextClosingPosition < html.Length && html[nextClosingPosition] != '>')
                {
                    nextClosingPosition++;
                }

                currentPosition++;

                bool isOpeningTag = true;
                if (html[currentPosition] == '/')
                {
                    isOpeningTag = false;
                    currentPosition++;
                }

                var tagCandidateContents = html.Substring(currentPosition, nextClosingPosition - currentPosition);
                if (_validTagParser.TryParse(tagCandidateContents, out IHtmlTag tag)) {
                    if (isOpeningTag)
                    {
                        htmlNodeBuilder.AddOpeningTag(tag);
                    } else
                    {
                        htmlNodeBuilder.AddClosingTag(tag);
                    }
                    currentPosition = nextClosingPosition + 1; // carry on looking for more tags after end of this one
                }
            }

            return htmlNodeBuilder.ToHtmlNodes();
        }
    }
}

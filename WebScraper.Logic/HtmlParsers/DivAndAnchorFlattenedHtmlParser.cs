using System;
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

        public IReadOnlyList<IHtmlNode> ParseHtml_OG(string html)
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

        public IReadOnlyList<IHtmlNode> ParseHtml(string html)
        {
            IReadOnlyList<IHtmlNode> res = new List<HtmlNode>();
            try
            {
                res = ParseHtmlUnSafe(html);
            } catch (Exception e)
            {
                var stop = true;
            }
            return res;
        }

        public IReadOnlyList<IHtmlNode> ParseHtmlUnSafe(string html)
        {
            var htmlNodeBuilder = new HtmlNodeBuilder(_tagFactory);
            var validTagParser = new ValidTagParser();

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
                if (validTagParser.TryParse(tagContents, out HtmlTag tag)) {
                    if (isOpeningTag)
                    {
                        var openingTag = tag.ToOpeningTag();
                        htmlNodeBuilder.AddOpeningTag(openingTag);
                    } else
                    {
                        var closingTag = tag.ToClosingTag();
                        htmlNodeBuilder.AddClosingTag(closingTag);
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

    public class ValidTagParser
    {
        // Then can have all the "funny" logic I want in here??
        private static readonly IList<string> _acceptedTags = new List<string>()
        {
            "div", "a"
        };

        private static readonly IList<char> _acceptableCharsProceedingTagNam = new List<char>()
        {
            ' ', '>', '\n', '\r'
        };

        public bool TryParse(string tagContents, out HtmlTag tag)
        {
            // if we can pass, and accept that the tagContents are valid then cool, we return. If not, then 
            var currentPos = 0;

            // Hmm. I _think_ I need  regex here.
            bool startsWithDivOrAnchor = false;
            string tagName = null;
            string attributes = "";
            foreach (var acceptedTag in _acceptedTags)
            {
                if (tagContents.StartsWith(acceptedTag))
                {
                    var endOfTagNamePos = acceptedTag.Length - 1;
                    if (string.Equals(tagContents, acceptedTag) || IsAcceptableTagProceedingTagName(tagContents[endOfTagNamePos + 1]))
                    {
                        startsWithDivOrAnchor = true;
                        tagName = acceptedTag;
                        var startOfAttributePos = endOfTagNamePos + 2;
                        if (startOfAttributePos < tagContents.Length - 1)
                        {
                            attributes = tagContents.Substring(startOfAttributePos); // all the way to the end
                        }
                        tag = new HtmlTag(tagName, attributes);
                        return true;
                        
                    }
                    
                }
            }

            tag = null;
            return false;

        }

        private bool IsAcceptableTagProceedingTagName(char inputChar)
        {
            return _acceptableCharsProceedingTagNam.Contains(inputChar);
        }
    }

    public class HtmlTag
    {
        // just to get builds working for now..
        public OpeningTag ToOpeningTag()
        {
            return new OpeningTag(Name, Attributes);
        }

        public ClosingTag ToClosingTag()
        {
            return new ClosingTag(Name); // okay just name for now I guess...??
        }

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

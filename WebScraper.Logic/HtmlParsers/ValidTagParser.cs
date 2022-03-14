using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public class ValidTagParser : IValidTagParser
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
}

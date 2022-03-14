using System.Collections.Generic;
using System.Linq;

namespace WebScraper.Logic.HtmlParsers
{
    /// <summary>
    /// Parsers out div and a tags if the tagCandidateContent meets some basic
    /// validation criteria
    /// </summary>
    public class ValidTagParser : IValidTagParser
    {
        private static readonly IEnumerable<string> _acceptedTags = new List<string>()
        {
            "div", "a"
        };

        private static readonly IEnumerable<char> _acceptableCharsProceedingTagNam = new List<char>()
        {
            ' ', '\n', '\r'
        };

        public bool TryParse(string tagCandidateContents, out IHtmlTag tag)
        {
            foreach (var acceptedTag in _acceptedTags)
            {
                if (tagCandidateContents.StartsWith(acceptedTag))
                {
                    var endOfTagNamePos = acceptedTag.Length - 1;
                    if (string.Equals(tagCandidateContents, acceptedTag) || IsAcceptableTagProceedingTagName(tagCandidateContents[endOfTagNamePos + 1]))
                    {
                        var tagName = acceptedTag;
                        var attributes = "";
                        var startOfAttributePos = endOfTagNamePos + 2;
                        if (startOfAttributePos < tagCandidateContents.Length - 1)
                        {
                            attributes = tagCandidateContents.Substring(startOfAttributePos); // all the way to the end
                        }
                        tag = new HtmlTag(acceptedTag, attributes);
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

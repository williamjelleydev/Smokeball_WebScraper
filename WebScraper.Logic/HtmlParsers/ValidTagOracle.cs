using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public class ValidTagOracle
    {
        // TODO: this could _probably_ be the config options of this oracle??
        private static readonly IList<string> _acceptedTags = new List<string>()
        {
            "div", "a"
        };

        private static readonly IList<char> _acceptableCharsProceedingTagNam = new List<char>()
        {
            ' ', '>', '\n', '\r'
        };

        // TODO: this needs rigourous checks (and tests) against overflowing the end of html
        // This has _way_ too much going on in out parameter :(
        public bool TryGetNextValidTag(int currentPosition, string html, out string tagContents, out bool isOpening, out int nextPosition)
        {
            // TODO: combine this with other while loop? lol

            while (currentPosition < html.Length)
            {
                while (currentPosition < html.Length && html[currentPosition] != '<') // these checks are getting real hacky aye
                {
                    // Next tag must atleast start with '<'
                    currentPosition++;
                }

                if (currentPosition >= html.Length)
                {
                    break; // Hacky, but I think will brack out and return..
                }

                currentPosition++;
                isOpening = true;
                if (html[currentPosition] == '/')
                {
                    isOpening = false;
                    currentPosition++;
                }

                // TODO: this is a gross, inefficient way of checking is the rest of the html.StartsWith() those 4 or so chars..
                var remainingSubstring = html.Substring(currentPosition, html.Length - currentPosition); // [0, 1, 2, 3]

                // TODO: this whole check is backwards
                bool isValidStartOfTag = false;
                foreach (var acceptedTag in _acceptedTags)
                {
                    if (remainingSubstring.StartsWith(acceptedTag))
                    {
                        // Further check that next char is an acceptable following char
                        var currentPositionTemp = currentPosition;
                        var endOfAcceptedTagPos = currentPosition + acceptedTag.Length;
                        if (_acceptableCharsProceedingTagNam.Contains(html[endOfAcceptedTagPos]))
                        {
                            // Now we can return!
                            isValidStartOfTag = true;
                            break;
                        }
                    }
                }

                if (isValidStartOfTag)
                {
                    var endTagPos = currentPosition;
                    while (html[endTagPos + 1] != '>')
                    {
                        endTagPos++;
                    }

                    var currentChar = html[currentPosition]; // debugging
                    tagContents = html.Substring(currentPosition, endTagPos - currentPosition + 1);
                    nextPosition = endTagPos + 2; // I think??

                    return true;
                }
            }

            tagContents = null;
            isOpening = false;
            nextPosition = 0;
            return false;
        }
    }
}

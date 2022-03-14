using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebScraper.Logic.HtmlParsers
{
    public class TagFactory : ITagFactory
    {
        // TODO: Proper parsing of tag contents.. It _could_ make sense to pass the opening and closing brackets in here as well then...?
        //public OpeningTag CreateOpeningTagFromContents_OG(string tagContents)
        //{
        //    // Need to find name, and Attributes.
        //    var separators = new char[] { ' ', '\r', '\n' };
        //    var allProps = tagContents.Split(separators, StringSplitOptions.RemoveEmptyEntries); // and perhaps newline chars etc in here as well??
        //    var name = allProps[0];
        //    var attributes = allProps.Skip(1).ToList(); // ew gross
        //    return new OpeningTag(name, attributes);
        //}

        public OpeningTag CreateOpeningTagFromContents(string tagContents)
        {
            var currentPos = 0;
            while (currentPos + 1 < tagContents.Length && !IsAcceptableCharProceedingTagName(tagContents[currentPos + 1]))
            {
                currentPos++;
            }

            var tagName = tagContents.Substring(0, currentPos + 1);
            // then attributes is whatever is left...
            var attributes = "";
            currentPos++;
            if (currentPos < tagContents.Length)
            {
                attributes = tagContents.Substring(currentPos, tagContents.Length - currentPos);
            }

            return new OpeningTag(tagName, attributes);
        }

        private static readonly IList<char> _acceptableCharsProceedingTagNam = new List<char>()
        {
            ' ', '\n', '\r'
        };

        private bool IsAcceptableCharProceedingTagName(char inputChar)
        {
            return _acceptableCharsProceedingTagNam.Contains(inputChar);
        }

        public ClosingTag CreateClosingTagFromContents(string tagContents)
        {
            // ew - this code is not a good fit for what we need here...
            var separators = new char[] { ' ', '\r', '\n' };
            var allProps = tagContents.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var name = allProps[0];
            return new ClosingTag(name);
        }

        // TODO: see if logic like this is necessary to make this parsing more robust?
        private bool IsValidTagChar(char inputChar)
        {
            return char.IsLetterOrDigit(inputChar) || inputChar == '-'; // even though we don't want dashes here lol
        }

        // Ew gross, but gets otehr dependencies in HtmlNodeBuilder working for now..
        public OpeningTag CreateOpeningTagFromClosingTag(ClosingTag closingTag)
        {
            // yeah ew..
            return new OpeningTag(closingTag.Name, "");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebScraper.Logic.HtmlParsers
{
    public class TagFactory : ITagFactory
    {
        // TODO: Proper parsing of tag contents.. It _could_ make sense to pass the opening and closing brackets in here as well then...?
        public OpeningTag CreateOpeningTagFromContents(string tagContents)
        {
            // Need to find name, and Attributes.
            var separators = new char[] { ' ', '\r', '\n' };
            var allProps = tagContents.Split(separators, StringSplitOptions.RemoveEmptyEntries); // and perhaps newline chars etc in here as well??
            var name = allProps[0];
            var attributes = allProps.Skip(1).ToList(); // ew gross
            return new OpeningTag(name, attributes);
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
            return new OpeningTag(closingTag.Name, new List<string>());
        }
    }
}

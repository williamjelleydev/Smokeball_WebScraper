using System;
using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public class TagFactory : ITagFactory
    {
        //// TODO: merge this logic with the ValidHtmlOracle somehow???
        //public IOpeningTag CreateOpeningTagFromContents(string tagContents)
        //{
        //    var currentPos = 0;
        //    while (currentPos + 1 < tagContents.Length && !IsAcceptableCharProceedingTagName(tagContents[currentPos + 1]))
        //    {
        //        currentPos++;
        //    }

        //    var tagName = tagContents.Substring(0, currentPos + 1);
        //    // then attributes is whatever is left...
        //    var attributes = "";
        //    currentPos++;
        //    if (currentPos < tagContents.Length)
        //    {
        //        attributes = tagContents.Substring(currentPos, tagContents.Length - currentPos);
        //    }

        //    return new OpeningTag(tagName, attributes);
        //}

        //private static readonly IList<char> _acceptableCharsProceedingTagNam = new List<char>()
        //{
        //    ' ', '\n', '\r'
        //};

        //private bool IsAcceptableCharProceedingTagName(char inputChar)
        //{
        //    return _acceptableCharsProceedingTagNam.Contains(inputChar);
        //}

        //public IClosingTag CreateClosingTagFromContents(string tagContents)
        //{
        //    // TODO: accept that a closing tag could have attributes (if self closing), and so create in the same way??
        //    // ew - this code is not a good fit for what we need here...
        //    var separators = new char[] { ' ', '\r', '\n' };
        //    var allProps = tagContents.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        //    var name = allProps[0];
        //    return new ClosingTag(name);
        //}

        //// TODO: see if logic like this is necessary to make this parsing more robust?
        //private bool IsValidTagChar(char inputChar)
        //{
        //    return char.IsLetterOrDigit(inputChar) || inputChar == '-'; // even though we don't want dashes here lol
        //}

        //// Ew gross, but gets otehr dependencies in HtmlNodeBuilder working for now..
        //public IOpeningTag CreateOpeningTagFromClosingTag(IClosingTag closingTag)
        //{
        //    // yeah ew..
        //    return new OpeningTag(closingTag.Name, "");
        //}
    }
}

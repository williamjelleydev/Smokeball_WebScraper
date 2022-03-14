﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Logic.HtmlParsers
{
    public class HtmlNodeBuilder
    {
        private readonly Stack<IOpeningTag> _unclosedOpeningTags = new Stack<IOpeningTag>();
        private readonly IList<IHtmlNode> _rootNodes = new List<IHtmlNode>();
        private readonly ITagFactory _tagFactory;

        public HtmlNodeBuilder(ITagFactory tagFactory)
        {
            _tagFactory = tagFactory;
        }

        public void AddOpeningTag(IOpeningTag tag)
        {
            _unclosedOpeningTags.Push(tag);
        }

        public void AddClosingTag(IClosingTag closingTag)
        {
            // TODO: I'm sure I can reduce this. There is a lot of similar/duplicate looking logic
            if (_unclosedOpeningTags.Count > 0)
            {
                var matchingOpeningTag = _unclosedOpeningTags.Peek();
                if (matchingOpeningTag.Name == closingTag.Name)
                {
                    _unclosedOpeningTags.Pop();
                    if (_unclosedOpeningTags.Count > 0)
                    {
                        // Add to top of stacks Children
                        var htmlNode = new HtmlNode(matchingOpeningTag);
                        _unclosedOpeningTags.Peek().Children.Add(htmlNode);
                    }
                    else
                    {
                        // root, so add to result list
                        var htmlNode = new HtmlNode(matchingOpeningTag);
                        _rootNodes.Add(htmlNode);
                    }
                }
                else
                {
                    var selfClosingTag = _unclosedOpeningTags.Pop();
                    var selfClosingHtmlNode = new HtmlNode(selfClosingTag);
                    if (_unclosedOpeningTags.Count > 0)
                    {
                        _unclosedOpeningTags.Peek().Children.Add(selfClosingHtmlNode);
                    }
                    else
                    {
                        _rootNodes.Add(selfClosingHtmlNode);
                    }

                    // Now try and re-process our tagContents
                    AddClosingTag(closingTag);
                }
            }
            else
            {
                var openingTag = _tagFactory.CreateOpeningTagFromClosingTag(closingTag);
                _rootNodes.Add(new HtmlNode(openingTag));
            }
        }

        // TODO: make a readonly list
        public IList<IHtmlNode> ToHtmlNodes()
        {
            return _rootNodes;
        }
    }
}

using System.Collections.Generic;

namespace WebScraper.Logic.HtmlParsers
{
    public class HtmlNodeBuilder
    {
        private readonly Stack<IOpeningTag> _unclosedOpeningTags = new Stack<IOpeningTag>();
        private readonly List<IHtmlNode> _rootNodes = new List<IHtmlNode>();
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
            if (_unclosedOpeningTags.Count > 0)
            {
                var matchingOpeningTag = _unclosedOpeningTags.Peek();
                if (matchingOpeningTag.Name == closingTag.Name)
                {
                    var htmlNode = new HtmlNode(_unclosedOpeningTags.Pop());
                    AddToParentOrRoot(htmlNode);
                }
                else
                {
                    var selfClosingHtmlNode = new HtmlNode(_unclosedOpeningTags.Pop());
                    AddToParentOrRoot(selfClosingHtmlNode);

                    // Now try and re-process our closingTag
                    AddClosingTag(closingTag);
                }
            }
            else
            {
                // Assume must be self closing tag
                var openingTag = _tagFactory.CreateOpeningTagFromClosingTag(closingTag);
                _rootNodes.Add(new HtmlNode(openingTag));
            }

            void AddToParentOrRoot(HtmlNode htmlNode)
            {
                if (_unclosedOpeningTags.Count > 0)
                {
                    _unclosedOpeningTags.Peek().Children.Add(htmlNode);
                } else
                {
                    _rootNodes.Add(htmlNode);
                }
            }
        }

        public IReadOnlyList<IHtmlNode> ToHtmlNodes()
        {
            return _rootNodes;
        }
    }
}

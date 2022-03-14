namespace WebScraper.Logic.HtmlParsers
{
    public class ClosingTag : IClosingTag
    {
        public ClosingTag(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}

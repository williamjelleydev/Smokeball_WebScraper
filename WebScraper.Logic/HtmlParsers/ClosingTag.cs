namespace WebScraper.Logic.HtmlParsers
{
    // TODO: base HtmlTag class?
    public class ClosingTag
    {
        public ClosingTag(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}

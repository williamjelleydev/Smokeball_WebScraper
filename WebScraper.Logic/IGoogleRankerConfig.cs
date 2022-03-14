using System.Collections.Generic;

namespace WebScraper.Logic
{
    public interface IGoogleRankerConfig
    {
        string DesiredUrl { get; }
        string GoogleSearchUrl { get; }
        IEnumerable<string> IdentifyingParentDivClasses { get; }
        int MaxResults { get; }
    }
}
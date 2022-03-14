using System.Collections.Generic;

namespace WebScraper.Logic
{
    public class GoogleRankerDefaultConfig : IGoogleRankerConfig
    {
        public string GoogleSearchUrl { get; } = "https://www.google.com.au/search?num=100&q=conveyancing+software";
        public string DesiredUrl { get; } = "www.smokeball.com.au";
        public IEnumerable<string> IdentifyingParentDivClasses { get; } = new List<string>()
        {
            "egMi0", "v5yQqb"
        };

        public int MaxResults { get; } = 100;
    }
}

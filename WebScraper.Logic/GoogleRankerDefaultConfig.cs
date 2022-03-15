using System.Collections.Generic;

namespace WebScraper.Logic
{
    // TODO: pass this in via appsettings.json so is more easily configurable
    public class GoogleRankerDefaultConfig : IGoogleRankerConfig
    {
        public string GoogleSearchUrl { get; } = "https://www.google.com.au/search?num=100&q=conveyancing+software";
        public string DesiredUrl { get; } = "www.smokeball.com.au";
        
        // Warning: As with any web scraper, this could be subject to change at any point..
        public IEnumerable<string> IdentifyingParentDivClasses { get; } = new List<string>()
        {
            "egMi0", "v5yQqb"
        };

        public int MaxResults { get; } = 100;
    }
}
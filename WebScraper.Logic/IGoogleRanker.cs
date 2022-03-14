using System.Collections.Generic;

namespace WebScraper.Logic
{
    public interface IGoogleRanker
    {
        IEnumerable<int> GetRankings();
    }
}
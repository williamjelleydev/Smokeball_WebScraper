using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebScraper.Logic
{
    public interface IGoogleRanker
    {
        IEnumerable<int> GetRankings();

        Task<IEnumerable<int>> GetRankingsAsync();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebScraper.Logic
{
    public interface IGoogleRanker
    {
        Task<IEnumerable<int>> GetRankingsAsync();
    }
}
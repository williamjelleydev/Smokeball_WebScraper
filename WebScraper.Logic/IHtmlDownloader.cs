using System.Threading.Tasks;

namespace WebScraper.Logic
{
    public interface IHtmlDownloader
    {
        Task<string> DownloadHtmlAsync(string url);
    }
}
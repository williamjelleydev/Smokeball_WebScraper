using System.Threading.Tasks;

namespace WebScraper.Logic
{
    public interface IHtmlDownloader
    {
        string DownloadHtml(string url);

        Task<string> DownloadHtmlAsync(string url);
    }
}
using System;
using System.Net;
using System.Threading.Tasks;

namespace WebScraper.Logic
{
    public class HtmlDownloader : IHtmlDownloader
    {
        public async Task<string> DownloadHtmlAsync(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                return await webClient.DownloadStringTaskAsync(url);
            }
        }
    }
}

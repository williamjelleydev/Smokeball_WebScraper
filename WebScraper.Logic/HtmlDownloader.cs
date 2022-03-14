using System;
using System.Net;
using System.Threading.Tasks;

namespace WebScraper.Logic
{
    public class HtmlDownloader : IHtmlDownloader
    {
        public async Task<string> DownloadHtmlAsync(string url)
        {
            string html = "";

            using (WebClient webClient = new WebClient())
            {
                html = await webClient.DownloadStringTaskAsync(url);
            }

            return html;
        }
    }
}

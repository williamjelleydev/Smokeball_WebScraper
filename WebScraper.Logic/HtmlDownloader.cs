using System.Net;

namespace WebScraper.Logic
{
    public class HtmlDownloader : IHtmlDownloader
    {
        // TODO: make this asyncSurely
        public string DownloadHtml(string url)
        {
            string html = "";
            // TODO: use factory for this?? Probably don't need to abstract away that far tbh..
            using (WebClient webClient = new WebClient())
            {
                html = webClient.DownloadString(url);
            }

            return html;
        }
    }
}

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebScraper.Logic.HtmlParsers;

namespace WebScraper.Logic
{
    public class GoogleRanker : IGoogleRanker
    {

        private readonly IHtmlDownloader _htmlDownloader;
        private readonly IHtmlParser _htmlParser;
        private readonly ILogger _logger;
        private readonly string _googleSearchUrl = "https://www.google.com.au/search?num=100&q=conveyancing+software"; // TODO: pass this in via config..? or at least the search terms might be good?
        private readonly string _identifyingParentDivClass = "egMi0";
        private readonly string _smokeBallUrl = "smokeball.com.au"; // TODO: pass this in I guess...?

        public GoogleRanker(
            IHtmlDownloader htmlDownloader,
            IHtmlParser htmlParser,
            ILogger<GoogleRanker> logger)
        {
            _htmlDownloader = htmlDownloader;
            _htmlParser = htmlParser;
            _logger = logger;
        }

        // TODO: make async, pass in some search terms maybe??
        public IEnumerable<int> GetRankings()
        {
            var html = _htmlDownloader.DownloadHtml(_googleSearchUrl); // TODO await this!
            var htmlNodes = _htmlParser.ParseHtml(html);

            _logger.LogWarning("DUH!: Inside GetRankings and testing MY LOOOOGGGGEEEERRR");
            Console.WriteLine("Here is a direct console writeline for comparison");

            var searchPositions = new List<int>();
            var currentSearchPosition = 0;
            // TODO: GetNodesWithClass() would probably be a cleaner method call here?
            foreach (var htmlNode in htmlNodes.SelectMany(x => x.GetNodesWithAttributesThatMatch(_identifyingParentDivClass)))
            {
                currentSearchPosition++;
                // TODO: make sure these are limited to top 100
                var anchorNode = htmlNode.Children.FirstOrDefault(x => x.Name == "a");
                if (anchorNode != null)
                {
                    if (anchorNode.HasHrefWithUrl(_smokeBallUrl))
                    {
                        searchPositions.Add(currentSearchPosition);
                    }
                }
                else
                {
                    _logger.LogWarning($"Html node with identifier '{_identifyingParentDivClass}' did not have child anchor tag.");
                }


            }

            return searchPositions;

        }
    }
}

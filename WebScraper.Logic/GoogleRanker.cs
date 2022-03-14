using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebScraper.Logic.HtmlParsers;

namespace WebScraper.Logic
{
    public class GoogleRanker : IGoogleRanker
    {

        // TODO: alot of this could be passed in via config pls
        private readonly IHtmlDownloader _htmlDownloader;
        private readonly IHtmlParser _htmlParser;
        private readonly ILogger _logger;
        private readonly string _googleSearchUrl = "https://www.google.com.au/search?num=100&q=conveyancing+software"; // TODO: pass this in via config..? or at least the search terms might be good?
        private readonly string _identifyingParentDivClass = "egMi0";
        private readonly string _identifyingParentAdDivClass = "v5yQqb";
        private readonly string _smokeBallUrl = "smokeball.com.au"; // TODO: pass this in I guess...?

        private readonly IEnumerable<string> _identifyingParentDivClasses = new List<string>()
        {
            "egMi0", "v5yQqb"
        };

        public GoogleRanker(
            IHtmlDownloader htmlDownloader,
            IHtmlParser htmlParser,
            ILogger<GoogleRanker> logger)
        {
            _htmlDownloader = htmlDownloader;
            _htmlParser = htmlParser;
            _logger = logger;
        }

        public async Task<IEnumerable<int>> GetRankingsAsync()
        {
            var html = await _htmlDownloader.DownloadHtmlAsync(_googleSearchUrl);
            var htmlNodes = _htmlParser.ParseHtml(html);

            var searchPositions = new List<int>();
            var currentSearchPosition = 0;
            // TODO: GetNodesWithClass() would probably be a cleaner method call here?

            // TODO: will have to change this to be able to pass in multiple classes at once??

            var identifyingClasses = new List<string>()
            {
                _identifyingParentDivClass, _identifyingParentAdDivClass
            };

            foreach (var htmlNode in htmlNodes.SelectMany(x => x.GetNodesWithAnyOfClasses(_identifyingParentDivClasses)).Take(100)) // TODO: make this 100 number configurable
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

        // TODO: get rid of this once debugging testing is complete..
        public async Task<List<UrlRanking>> GetAllRankingsAsync()
        {
            var html = await _htmlDownloader.DownloadHtmlAsync(_googleSearchUrl);

            var htmlNodes = _htmlParser.ParseHtml(html);

            //var searchPositions = new List<int>();
            var allRankings = new List<UrlRanking>();
            var currentSearchPosition = 0;
            // TODO: GetNodesWithClass() would probably be a cleaner method call here?

            // TODO: will have to change this to be able to pass in multiple classes at once??

            var identifyingClasses = new List<string>()
            {
                _identifyingParentDivClass, _identifyingParentAdDivClass
            };

            foreach (var htmlNode in htmlNodes.SelectMany(x => x.GetNodesWithAnyOfClasses(_identifyingParentDivClasses)).Take(100))
            {
                currentSearchPosition++;
                // TODO: make sure these are limited to top 100
                var anchorNode = htmlNode.Children.FirstOrDefault(x => x.Name == "a");
                if (anchorNode != null)
                {
                    //if (anchorNode.HasHrefWithUrl(_smokeBallUrl))
                    //{
                    //    searchPositions.Add(currentSearchPosition);
                    //}
                    allRankings.Add(new UrlRanking()
                    {
                        Url = "egh",
                        Ranking = currentSearchPosition,
                        AnchorNode = anchorNode
                    });
                }
                else
                {
                    _logger.LogWarning($"Html node with identifier '{_identifyingParentDivClass}' did not have child anchor tag.");
                }


            }

            return allRankings;
        }
    }

    public class UrlRanking
    {
        public string Url { get; set; }
        public int Ranking { get; set; }
        public HtmlNode AnchorNode { get; set; }
    }
}

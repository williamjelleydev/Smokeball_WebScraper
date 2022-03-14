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
        private readonly IHtmlDownloader _htmlDownloader;
        private readonly IHtmlParser _htmlParser;
        private readonly IGoogleRankerConfig _config;
        private readonly ILogger _logger;

        public GoogleRanker(
            IHtmlDownloader htmlDownloader,
            IHtmlParser htmlParser,
            IGoogleRankerConfig googleRankerConfig,
            ILogger<GoogleRanker> logger)
        {
            _htmlDownloader = htmlDownloader;
            _htmlParser = htmlParser;
            _config = googleRankerConfig;
            _logger = logger;
        }

        public async Task<IEnumerable<int>> GetRankingsAsync()
        {
            var html = await _htmlDownloader.DownloadHtmlAsync(_config.GoogleSearchUrl);
            var htmlNodes = _htmlParser.ParseHtml(html);

            var searchPositions = new List<int>();
            var currentSearchPosition = 0;

            foreach (var htmlNode in htmlNodes.SelectMany(x => x.GetNodesWithAnyOfClasses(_config.IdentifyingParentDivClasses)).Take(_config.MaxResults)) // TODO: make this 100 number configurable
            {
                currentSearchPosition++;
                var anchorNode = htmlNode.Children.FirstOrDefault(x => x.Name == "a");
                if (anchorNode != null)
                {
                    if (anchorNode.HasHrefWithUrl(_config.DesiredUrl))
                    {
                        searchPositions.Add(currentSearchPosition);
                    }
                }
                else
                {
                    _logger.LogWarning($"Html node did not have child anchor tag.");
                }
            }

            return searchPositions;
        }
    }
}

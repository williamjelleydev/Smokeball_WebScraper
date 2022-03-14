using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Logic.HtmlParsers;
using WebScraper.Logic.Tests.Integration.Customizations;
using Xunit;

namespace WebScraper.Logic.Tests.Integration
{
    public class GoogleRankerTests
    {
        public class GetRankings
        {
            [Theory]
            [AutoMoqData]
            public void OneSmokeballSearchResultAtPosition5(
                [Frozen] IHtmlDownloader htmlDownloader,
                ILogger<GoogleRanker> logger)
            {
                // Hmm. How do I ensure that I actually get concrete implementations of everything here??
                // For now, use concreate and manual ctro implementations of everything pls.
                // then after functional, figure out how to magic autofixture to do what I want..

                var expectedRanking = 5;

                var tagFactory = new TagFactory();
                var htmlParser = new HtmlParser(tagFactory);

                // TODO: make relative file path so can run anywhere..
                string htmlFilePath = @"C:\Source\WebScraper\WebScraper\WebScraper.Logic.Tests.Integration\TestData\GoogleTest.html";
                var html = File.ReadAllText(htmlFilePath);
                Mock.Get(htmlDownloader).Setup(x => x.DownloadHtml(It.IsAny<string>())).Returns(html);

                var sut = new GoogleRanker(htmlDownloader, htmlParser, logger);

                var res = sut.GetRankings();

                res.Should().HaveCount(1);
                res.First().Should().Be(expectedRanking);
            }

            [Theory (Skip ="Enable once code actually meets this criteria lol")]
            [AutoMoqData]
            public void OneSmokeballSearchResultAtPosition7(
                [Frozen] IHtmlDownloader htmlDownloader,
                ILogger<GoogleRanker> logger)
            {
                var expectedRanking = 7;

                var tagFactory = new TagFactory();
                var htmlParser = new HtmlParser(tagFactory);

                // TODO: make relative file path so can run anywhere..
                string htmlFilePath = @"C:\Source\WebScraper\WebScraper\WebScraper.Logic.Tests.Integration\TestData\GoogleTest.html";
                var html = File.ReadAllText(htmlFilePath);
                Mock.Get(htmlDownloader).Setup(x => x.DownloadHtml(It.IsAny<string>())).Returns(html);

                var sut = new GoogleRanker(htmlDownloader, htmlParser, logger);

                var res = sut.GetRankings();

                res.Should().HaveCount(1);
                res.First().Should().Be(expectedRanking);
            }

        }
    }
}

using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebScraper.Logic.HtmlParsers;
using WebScraper.Logic.Tests.Unit.Customizations;
using Xunit;

namespace WebScraper.Logic.Tests.Unit
{
    public class GoogleRankerTests
    {
        public class GetRankingsAsync
        {
            [Theory]
            [AutoMoqData]
            public async Task ReturnsEmptyWhenHtmlIsEmpty(
                [Frozen] IHtmlDownloader htmlDownloader,
                GoogleRanker sut)
            {
                Mock.Get(htmlDownloader).Setup(x => x.DownloadHtmlAsync(It.IsAny<string>())).Returns(Task.FromResult(""));

                var res = await sut.GetRankingsAsync();

                res.Should().BeEmpty();
            }


            [Theory]
            [AutoMoqData]
            public async Task ReturnsEmptyWhenNoNodesParsed(
                [Frozen] IHtmlParser htmlParser,
                GoogleRanker sut)
            {
                var nodes = new List<IHtmlNode>();
                Mock.Get(htmlParser).Setup(x => x.ParseHtml(It.IsAny<string>())).Returns(nodes);

                var res = await sut.GetRankingsAsync();

                res.Should().BeEmpty();
            }

            // TODO: finish implementing tests


        }
    }
}

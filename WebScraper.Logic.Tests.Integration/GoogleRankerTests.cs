using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebScraper.Logic.HtmlParsers;
using WebScraper.Logic.Tests.Integration.Customizations;
using Xunit;

namespace WebScraper.Logic.Tests.Integration
{
    public class GoogleRankerTests
    {
        public class GetRankingsAsync
        {
            [Theory]
            [AutoMoqData]
            public async Task OneSmokeballSearchResultAtPosition5(
                [Frozen] IFixture fixture,
                [Frozen] IHtmlDownloader htmlDownloader,
                TagFactory tagFactory)
            {
                var expectedRanking = 5;

                fixture.Register<ITagFactory>(() => tagFactory);
                fixture.Register<IValidTagOracle>(() => fixture.Create<ValidTagOracle>());
                fixture.Register<IHtmlParser>(() => fixture.Create<DivAndAnchorFlattenedHtmlParser>());

                // TODO: make relative file path so can run anywhere..
                string htmlFilePath = @"C:\Source\WebScraper\WebScraper\WebScraper.Logic.Tests.Integration\TestData\GoogleTest.html";
                var html = File.ReadAllText(htmlFilePath);
                Mock.Get(htmlDownloader).Setup(x => x.DownloadHtmlAsync(It.IsAny<string>())).Returns(Task.FromResult(html));

                var sut = fixture.Create<GoogleRanker>();

                var result = await sut.GetRankingsAsync();

                using (new AssertionScope())
                {
                    result.Should().HaveCount(1);
                    result.First().Should().Be(expectedRanking);
                }
            }


            [Theory(Skip = "Enable once code actually meets this criteria lol")]
            [AutoMoqData]
            public async Task OneSmokeballSearchResultAtPosition7(
                [Frozen] IFixture fixture,
                [Frozen] IHtmlDownloader htmlDownloader,
                TagFactory tagFactory)
            {
                var expectedRanking = 7;

                fixture.Register<ITagFactory>(() => tagFactory);
                fixture.Register<IValidTagOracle>(() => fixture.Create<ValidTagOracle>());
                fixture.Register<IHtmlParser>(() => fixture.Create<DivAndAnchorFlattenedHtmlParser>());

                // TODO: make relative file path so can run anywhere..
                string htmlFilePath = @"C:\Source\WebScraper\WebScraper\WebScraper.Logic.Tests.Integration\TestData\GoogleTest.html";
                var html = File.ReadAllText(htmlFilePath);
                Mock.Get(htmlDownloader).Setup(x => x.DownloadHtmlAsync(It.IsAny<string>())).Returns(Task.FromResult(html));

                var sut = fixture.Create<GoogleRanker>();

                var result = await sut.GetRankingsAsync();

                using (new AssertionScope())
                {
                    result.Should().HaveCount(1);
                    result.First().Should().Be(expectedRanking);
                }
            }
        }
    }
}

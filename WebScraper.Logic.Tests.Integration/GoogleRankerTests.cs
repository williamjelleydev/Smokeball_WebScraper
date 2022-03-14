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
            public async Task OneSmokeballSearchResultAtPosition6(
                [Frozen] IFixture fixture,
                [Frozen] IHtmlDownloader htmlDownloader,
                ValidTagParser validTagParser,
                GoogleRankerDefaultConfig googleRankerConfig)
            {
                var expectedRanking = 6;

                fixture.Register<IValidTagParser>(() => validTagParser);
                fixture.Register<IHtmlParser>(() => fixture.Create<DivAndAnchorFlattenedHtmlParser>());
                fixture.Register<IGoogleRankerConfig>(() => googleRankerConfig);

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

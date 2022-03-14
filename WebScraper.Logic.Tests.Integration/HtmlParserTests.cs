using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using FluentAssertions.Execution;
using System.IO;
using WebScraper.Logic.HtmlParsers;
using WebScraper.Logic.Tests.Integration.Customizations;
using Xunit;

namespace WebScraper.Logic.Tests.Integration
{
    public class HtmlParserTests
    {
        public class ParseHtml
        {
            [Theory]
            [AutoMoqData]
            public void FlattensToOnlyDivsAndAnchors(
                TagFactory tagFactory,
                [Frozen] IFixture fixture)
            {
                // TODO: make this a relative path so is not machine dependent..
                string htmlFilePath = @"C:\Source\WebScraper\WebScraper\WebScraper.Logic.Tests.Integration\TestData\SampleHtml1.html";
                var html = File.ReadAllText(htmlFilePath);

                fixture.Register<ITagFactory>(() => tagFactory);

                var sut = fixture.Create<HtmlParser>();

                var htmlNodes = sut.ParseHtml(html);

                using (new AssertionScope())
                {
                    htmlNodes.Should().HaveCount(2);
                    htmlNodes[0].Name.Should().Be("div");
                    htmlNodes[0].Children.Should().HaveCount(2);
                    htmlNodes[0].Children[0].Name.Should().Be("div");
                    htmlNodes[0].Children[1].Name.Should().Be("a");
                }
            } 
        }
    }
}
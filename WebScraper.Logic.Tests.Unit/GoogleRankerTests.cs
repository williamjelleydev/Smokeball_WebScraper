using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
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
            public async Task ReturnsEmptyWhenNoHtmlNodes(
                [Frozen] IHtmlParser htmlParser,
                GoogleRanker sut)
            {
                var nodes = new List<IHtmlNode>();
                Mock.Get(htmlParser).Setup(x => x.ParseHtml(It.IsAny<string>())).Returns(nodes);

                var res = await sut.GetRankingsAsync();

                res.Should().BeEmpty();
            }

            [Theory]
            [AutoMoqData]
            public async Task ReturnsEmptyWhenNoNodesWithIdentifyingDivClass(
                [Frozen] IHtmlParser htmlParser,
                IReadOnlyList<IHtmlNode> parsedNodes,
                [Frozen] IGoogleRankerConfig config,
                GoogleRanker sut)
            {

                foreach (var parsedNode in parsedNodes)
                {
                    Mock.Get(parsedNode).Setup(x => x.GetNodesWithAnyOfClasses(config.IdentifyingParentDivClasses)).Returns(new List<IHtmlNode>());
                }

                Mock.Get(htmlParser).Setup(x => x.ParseHtml(It.IsAny<string>())).Returns(parsedNodes);

                var res = await sut.GetRankingsAsync();

                res.Should().BeEmpty();
            }
            
            [Theory]
            [AutoMoqData]
            public async Task ReturnsEmptyWhenIdentifyingNodesHaveNoAnchorNode(
                [Frozen] IHtmlParser htmlParser,
                IReadOnlyList<IHtmlNode> parsedNodes,
                IReadOnlyList<IHtmlNode> nodesWithIdentifyingClasses,
                IEnumerable<string> identifyingParentDivClasses,
                [Frozen] IGoogleRankerConfig config,
                GoogleRanker sut)
            {
                Mock.Get(config).SetupGet(x => x.IdentifyingParentDivClasses).Returns(identifyingParentDivClasses);
                Mock.Get(config).SetupGet(x => x.MaxResults).Returns(nodesWithIdentifyingClasses.Count); // Make sure they don;t get ignored..

                Mock.Get(parsedNodes.First()).Setup(x => x.GetNodesWithAnyOfClasses(config.IdentifyingParentDivClasses)).Returns(nodesWithIdentifyingClasses);
                foreach (var parsedNode in parsedNodes.Skip(1))
                {
                    Mock.Get(parsedNode).Setup(x => x.GetNodesWithAnyOfClasses(config.IdentifyingParentDivClasses)).Returns(new List<IHtmlNode>());
                }

                Mock.Get(htmlParser).Setup(x => x.ParseHtml(It.IsAny<string>())).Returns(parsedNodes);

                var res = await sut.GetRankingsAsync();

                res.Should().BeEmpty();
            }

            // TODO: tests to show _config.MaxResults filters out any extra results

            [Theory]
            [AutoMoqData]
            public async Task ReturnsRankingsOfDesiredUrlWithinNodesWithIdentifyingDivClasses(
                [Frozen] IHtmlParser htmlParser,
                IReadOnlyList<IHtmlNode> parsedNodes,
                IReadOnlyList<IHtmlNode> nodesWithIdentifyingClasses,
                IEnumerable<string> identifyingParentDivClasses,
                IHtmlNode nodeWithMatchingAnchor,
                [Frozen] IGoogleRankerConfig config,
                string desiredUrl,
                GoogleRanker sut)
            {
                Mock.Get(config).SetupGet(x => x.IdentifyingParentDivClasses).Returns(identifyingParentDivClasses);
                Mock.Get(config).SetupGet(x => x.MaxResults).Returns(nodesWithIdentifyingClasses.Count); // Make sure they don;t get ignored..
                Mock.Get(config).SetupGet(x => x.DesiredUrl).Returns(desiredUrl);

                Mock.Get(nodeWithMatchingAnchor).SetupGet(x => x.Name).Returns("a");
                Mock.Get(nodeWithMatchingAnchor).Setup(x => x.HasHrefWithUrl(desiredUrl)).Returns(true);

                var nodeWithChildWithMatchingAnchor = nodesWithIdentifyingClasses.Skip(1).First(); // expected res is in 2nd position..
                Mock.Get(nodeWithChildWithMatchingAnchor).SetupGet(x => x.Children).Returns(new List<IHtmlNode>() { nodeWithMatchingAnchor });
                

                Mock.Get(parsedNodes.First()).Setup(x => x.GetNodesWithAnyOfClasses(config.IdentifyingParentDivClasses)).Returns(nodesWithIdentifyingClasses);
                foreach (var parsedNode in parsedNodes.Skip(1))
                {
                    Mock.Get(parsedNode).Setup(x => x.GetNodesWithAnyOfClasses(config.IdentifyingParentDivClasses)).Returns(new List<IHtmlNode>());
                }

                Mock.Get(htmlParser).Setup(x => x.ParseHtml(It.IsAny<string>())).Returns(parsedNodes);

                var res = await sut.GetRankingsAsync();

                res.Should().HaveCount(1);
                res.First().Should().Be(2);
            } 

            // TODO: add tests for each case/codepath in GoogleRanker
        }
    }
}

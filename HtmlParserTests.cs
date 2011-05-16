using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ClassLibrary3
{
    public class HtmlParserTests
    {
        [Fact]
        public void ParseNodeHtml()
        {
            const string html = "<html></html>";
            HtmlNode node = HtmlParser.Parse(html).First();
            Assert.Equal("html", node.Name);
        }

        [Fact]
        public void ParseNodeBody()
        {
            const string html = "<body></body>";
            HtmlNode node = HtmlParser.Parse(html).First();
            Assert.Equal("body", node.Name);
        }

        [Fact]
        public void Parse2Tags()
        {
            const string html = "<head></head><body></body>";
            IEnumerable<HtmlNode> nodes = HtmlParser.Parse(html);
            HtmlNode first = nodes.First();
            HtmlNode last = nodes.Last();
            Assert.Equal("head", first.Name);
            Assert.Equal("body", last.Name);
        }

        [Fact]
        public void ParseTextAndTag()
        {
            const string html = "<<head></head>";
            IEnumerable<HtmlNode> nodes = HtmlParser.Parse(html);
            HtmlNode first = nodes.First();
            HtmlNode last = nodes.Last();
            Assert.Equal("<", first.Name);
            Assert.Equal("head", last.Name);
        }

        [Fact]
        public void ParseNestedTags()
        {
            const string html = "<head><title></title></head>";
            IEnumerable<HtmlNode> nodes = HtmlParser.Parse(html);
            HtmlNode first = nodes.First();
            HtmlNode last = first.Nodes.Single();
            Assert.Equal("head", first.Name);
            Assert.Equal("title", last.Name);
        }
        [Fact]
        public void ParseAttribute()
        {
            const string html = "<head x></head>";
            IEnumerable<HtmlNode> nodes = HtmlParser.Parse(html);
            HtmlNode first = nodes.First();
            HtmlNode last = first.Nodes.Single();
            Assert.Equal("head", first.Name);
            Assert.Equal("title", last.Name);
        }
    }
}

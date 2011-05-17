namespace HtmlParser.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class TreeBuilderFacts
    {
        [Fact]
        public void Comment()
        {
            var comment = new Token(TokenType.Comment, "head");

            var tokens = new[]
                             {
                                 comment,
                             };

            HtmlNode node = TreeBuilder.BuildTree(tokens).First();
            Assert.Equal(HtmlNodeType.Comment, node.NodeType);
            Assert.Equal("head", node.Name);
        }

        [Fact]
        public void AttributeName()
        {
            var openTag = new Token(TokenType.OpenTag, "head");

            var attributeName = new Token(TokenType.AttributeName, "xxx");

            var closeTag = new Token(TokenType.CloseTag, "head");

            var tokens = new[]
                             {
                                 openTag,
                                 attributeName,
                                 closeTag
                             };

            HtmlNode node = TreeBuilder.BuildTree(tokens).First();
            HtmlAttribute attribute = node.Attributes.First();
            Assert.Equal("xxx", attribute.Name);
        }

        [Fact]
        public void AttributeValue()
        {
            var openTag = new Token(TokenType.OpenTag, "head");

            var attributeName = new Token(TokenType.AttributeName, "xxx");

            var attributeValue = new Token(TokenType.AttributeValue, "yyy");

            var closeTag = new Token(TokenType.CloseTag, "head");

            var tokens = new[]
                             {
                                 openTag,
                                 attributeName,
                                 attributeValue,
                                 closeTag
                             };

            HtmlNode node = TreeBuilder.BuildTree(tokens).First();
            HtmlAttribute attribute = node.Attributes.First();
            Assert.Equal("xxx", attribute.Name);
            Assert.Equal("yyy", attribute.Value);
        }

        [Fact]
        public void SelfClosingTagBr()
        {
            var br = new Token(TokenType.OpenTag, "br");

            var body = new Token(TokenType.OpenTag, "body");

            var tokens = new[]
                             {
                                 br,
                                 body,
                             };

            List<HtmlNode> nodes = TreeBuilder.BuildTree(tokens).ToList();
            Assert.Equal(2, nodes.Count);
        }

        [Fact]
        public void SelfClosingTagBrInBody()
        {
            var br = new Token(TokenType.OpenTag, "br");

            var body = new Token(TokenType.OpenTag, "body");

            var innerBr = new Token(TokenType.OpenTag, "br");

            var tokens = new[]
                             {
                                 br,
                                 body,
                                 innerBr,
                             };

            List<HtmlNode> nodes = TreeBuilder.BuildTree(tokens).ToList();
            Assert.Equal(2, nodes.Count);
        }

        [Fact]
        public void ThreeSelfClosingTagBr()
        {
            var br1 = new Token(TokenType.OpenTag, "br");
            var br2 = new Token(TokenType.OpenTag, "br");
            var br3 = new Token(TokenType.OpenTag, "br");

            var tokens = new[]
                             {
                                 br1,
                                 br2,
                                 br3,
                             };

            List<HtmlNode> nodes = TreeBuilder.BuildTree(tokens).ToList();
            Assert.Equal(3, nodes.Count);
        }
    }
}
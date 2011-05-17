namespace HtmlParser.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class TreeBuilderFacts
    {
        [Fact]
        public void AttributeName()
        {
            var openTag = new Token(TokenType.OpenTag);
            openTag.Builder.Append("head");

            var attributeName = new Token(TokenType.AttributeName);
            attributeName.Builder.Append("xxx");

            var closeTag = new Token(TokenType.CloseTag);
            closeTag.Builder.Append("head");

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
            var openTag = new Token(TokenType.OpenTag);
            openTag.Builder.Append("head");

            var attributeName = new Token(TokenType.AttributeName);
            attributeName.Builder.Append("xxx");

            var attributeValue = new Token(TokenType.AttributeValue);
            attributeValue.Builder.Append("yyy");

            var closeTag = new Token(TokenType.CloseTag);
            closeTag.Builder.Append("head");

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
            var br = new Token(TokenType.OpenTag);
            br.Builder.Append("br");

            var body = new Token(TokenType.OpenTag);
            body.Builder.Append("body");

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
            var br = new Token(TokenType.OpenTag);
            br.Builder.Append("br");

            var body = new Token(TokenType.OpenTag);
            body.Builder.Append("body");

            var innerBr = new Token(TokenType.OpenTag);
            innerBr.Builder.Append("br");

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
            var br1 = new Token(TokenType.OpenTag);
            br1.Builder.Append("br");
            var br2 = new Token(TokenType.OpenTag);
            br2.Builder.Append("br");
            var br3 = new Token(TokenType.OpenTag);
            br3.Builder.Append("br");

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
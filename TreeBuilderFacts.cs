using System;
using System.Linq;
using Xunit;

namespace ClassLibrary3
{
    public class TreeBuilderFacts
    {
        [Fact]
        public void AttributeName()
        {
            var openTag = new Token(TokenType.OpenTag);
            openTag.Builder.AppendLine("head");

            var attributeName = new Token(TokenType.AttributeName);
            attributeName.Builder.Append("xxx");

            var closeTag = new Token(TokenType.CloseTag);
            closeTag.Builder.AppendLine("head");

            var tokens = new[]
                             {
                                 openTag,
                                 attributeName,
                                 closeTag
                             };

            var node = TreeBuilder.BuildTree(tokens).First();
            var attribute = node.Attributes.First();
            Assert.Equal("xxx", attribute.Name);
        }
        [Fact]
        public void AttributeValue()
        {
            var openTag = new Token(TokenType.OpenTag);
            openTag.Builder.AppendLine("head");

            var attributeName = new Token(TokenType.AttributeName);
            attributeName.Builder.Append("xxx");

            var attributeValue = new Token(TokenType.AttributeValue);
            attributeValue.Builder.Append("yyy");

            var closeTag = new Token(TokenType.CloseTag);
            closeTag.Builder.AppendLine("head");

            var tokens = new[]
                             {
                                 openTag,
                                 attributeName,
                                 attributeValue,
                                 closeTag
                             };

            var node = TreeBuilder.BuildTree(tokens).First();
            var attribute = node.Attributes.First();
            Assert.Equal("xxx", attribute.Name);
            Assert.Equal("yyy", attribute.Value);
        }
    }
}

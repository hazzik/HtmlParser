using System;
using System.Linq;
using Xunit;

namespace ClassLibrary3
{
    public class TokenParserFacts
    {
        [Fact]
        public void ParseAttributeName()
        {
            const string html = "<head x>";
            var tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token last = tokens.Last();
            Assert.Equal(TokenType.AttributeName, last.Type);
            Assert.Equal("x", last.Value);
        }
        [Fact]
        public void ParseAttributeNameWithoutWhitespaces()
        {
            const string html = "<head x >";
            var tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token last = tokens.Last();
            Assert.Equal(TokenType.AttributeName, last.Type);
            Assert.Equal("x", last.Value);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ClassLibrary3
{
    public class TokenParserFacts
    {
        [Fact]
        public void ParseTextWithEquals()
        {
            const string html = "=";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Token first = tokens.First();
            Assert.Equal(first.Type, TokenType.Text);
            Assert.Equal("=", first.Value);
        }

        [Fact(Skip = "not now")]
        public void ParseTextWithLt()
        {
            const string html = "<";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Token first = tokens.First();
            Assert.Equal(first.Type, TokenType.Text);
            Assert.Equal("<", first.Value);
        }

        [Fact]
        public void ParseTextWithGt()
        {
            const string html = ">";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Token first = tokens.First();
            Assert.Equal(first.Type, TokenType.Text);
            Assert.Equal(">", first.Value);
        }

        [Fact]
        public void ParseTextWithSlash()
        {
            const string html = "/";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Token first = tokens.First();
            Assert.Equal(first.Type, TokenType.Text);
            Assert.Equal("/", first.Value);
        }

        [Fact]
        public void ParseTextWithSingleQuote()
        {
            const string html = "'";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Token first = tokens.First();
            Assert.Equal(first.Type, TokenType.Text);
            Assert.Equal("'", first.Value);
        }

        [Fact]
        public void ParseTextWithDoubleQuote()
        {
            const string html = "\"";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Token first = tokens.First();
            Assert.Equal(first.Type, TokenType.Text);
            Assert.Equal("\"", first.Value);
        }

        [Fact]
        public void ParseAttributeName()
        {
            const string html = "<head x>";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token last = tokens.Last();
            Assert.Equal(TokenType.AttributeName, last.Type);
            Assert.Equal("x", last.Value);
        }

        [Fact]
        public void ParseAttributeNameWithoutWhitespaces()
        {
            const string html = "<head x >";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token last = tokens.Last();
            Assert.Equal(TokenType.AttributeName, last.Type);
            Assert.Equal("x", last.Value);
        }

        [Fact]
        public void ParseTwoAttributeNames()
        {
            const string html = "<head x y>";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens[0].Value);
            Token firstAttr = tokens[1];
            Assert.Equal(TokenType.AttributeName, firstAttr.Type);
            Assert.Equal("x", firstAttr.Value);
            Token secondAttr = tokens[2];
            Assert.Equal(TokenType.AttributeName, secondAttr.Type);
            Assert.Equal("y", secondAttr.Value);
        }

        [Fact]
        public void ParseAttributeValue()
        {
            const string html = "<head x=y>";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeValueWithWhitespaces()
        {
            const string html = "<head x= y >";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeValueWithSlashInside()
        {
            const string html = "<head x=y/z>";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y/z", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeDoubleQuotedValue()
        {
            const string html = "<head x=\"y\">";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeDoubleQuotedValueWithWhitespaces()
        {
            const string html = "<head x=\"y z\">";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y z", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeDoubleQuotedValueWithSingleQuoteInside()
        {
            const string html = "<head x=\"y'z\">";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y'z", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeDoubleQuotedValueWithEqualsInside()
        {
            const string html = "<head x=\"y=z\">";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y=z", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeDoubleQuotedValueWithLtInside()
        {
            const string html = "<head x=\"y<z\">";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y<z", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeDoubleQuotedValueWithGtInside()
        {
            const string html = "<head x=\"y>z\">";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y>z", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeDoubleQuotedValueWithSlashInside()
        {
            const string html = "<head x=\"y/z\">";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y/z", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeSingleQuotedValue()
        {
            const string html = "<head x='y'>";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y", attrValue.Value);
        }
        
        [Fact]
        public void ParseAttributeSingleQuotedValueWithDoublequoteInside()
        {
            const string html = "<head x='y\"z'>";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y\"z", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeSingleQuotedValueWithEqualsInside()
        {
            const string html = "<head x='y=z'>";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y=z", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeSingleQuotedValueWithLtInside()
        {
            const string html = "<head x='y<z'>";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y<z", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeSingleQuotedValueWithGtInside()
        {
            const string html = "<head x='y>z'>";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y>z", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeSingleQuotedValueWithSlashInside()
        {
            const string html = "<head x='y/z'>";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);
            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);
            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y/z", attrValue.Value);
        }

        [Fact]
        public void ParseAttributeNameAfterAttributeValue()
        {
            const string html = "<head x=y z>";
            List<Token> tokens = TokenParser.Parse(html).ToList();
            Assert.Equal("head", tokens.First().Value);

            Token attrName = tokens[1];
            Assert.Equal(TokenType.AttributeName, attrName.Type);
            Assert.Equal("x", attrName.Value);

            Token attrValue = tokens[2];
            Assert.Equal(TokenType.AttributeValue, attrValue.Type);
            Assert.Equal("y", attrValue.Value);

            Token secondAttrName = tokens[3];
            Assert.Equal(TokenType.AttributeName, secondAttrName.Type);
            Assert.Equal("z", secondAttrName.Value);
        }
    }
}

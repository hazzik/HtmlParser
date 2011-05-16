using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xunit;

namespace ClassLibrary3
{
    public class Class1
    {
        [Fact]
        public void ParseNodeHtml()
        {
            const string html = "<html></html>";
            HtmlNode node = Parse(html);
            Assert.Equal("html", node.Name);
        }

        [Fact]
        public void ParseNodeBody()
        {
            const string html = "<body></body>";
            HtmlNode node = Parse(html);
            Assert.Equal("body", node.Name);
        }

        private static HtmlNode Parse(string html)
        {
            var tokens = ParseTokens(html);
            return BuildTree(tokens);
        }

        private static IEnumerable<Token> ParseTokens(string html)
        {
            var previousToken = new Token(TokenType.Text);
            char previousChar = '\0';
            ParseState state = ParseState.Text;
            var currentToken = new Token(TokenType.Text);
            var tokens = new List<Token> {currentToken};
            foreach (char ch in html)
            {
                if (ch == '<')
                {
                    switch (state)
                    {
                        case ParseState.Default:
                        case ParseState.Text:
                            previousToken = currentToken;
                            currentToken = new Token(TokenType.OpenTag);
                            tokens.Add(currentToken);
                            state = ParseState.Tag;
                            break;
                        case ParseState.Tag:
                            previousToken.Builder.Append(previousChar);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (ch == '>')
                {
                    switch (state)
                    {
                        case ParseState.Default:
                        case ParseState.Text:
                            currentToken.Builder.Append(ch);
                            break;
                        case ParseState.Tag:
                            previousToken = currentToken;
                            currentToken = new Token(TokenType.Text);
                            tokens.Add(currentToken);
                            state = ParseState.Default;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (ch == '/')
                {
                    switch (state)
                    {
                        case ParseState.Default:
                        case ParseState.Text:
                            currentToken.Builder.Append(ch);
                            break;
                        case ParseState.Tag:
                            currentToken.Type = TokenType.CloseTag;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    currentToken.Builder.Append(ch);
                }
                previousChar = ch;
            }
            return tokens;
        }

        private static HtmlNode BuildTree(IEnumerable<Token> tokens)
        {
            var tree = new List<HtmlNode>();
            var currentNode = new HtmlNode();
            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.OpenTag:
                        currentNode.Name = token.Builder.ToString();
                        break;
                    case TokenType.CloseTag:
                        tree.Add(currentNode);
                        currentNode = new HtmlNode();
                        break;
                    case TokenType.AttributeName:
                        break;
                    case TokenType.AttributeValue:
                        break;
                    case TokenType.Text:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return tree.First();
        }

        #region Nested type: HtmlNode

        private class HtmlNode
        {
            public string Name { get; set; }
        }

        #endregion

        #region Nested type: ParseState

        private enum ParseState
        {
            Default,
            Text,
            Tag,
        }

        #endregion

        #region Nested type: Token

        [DebuggerDisplay("{Builder}")]
        private class Token
        {
            private readonly StringBuilder builder = new StringBuilder();

            public Token(TokenType type)
            {
                Type = type;
            }

            public TokenType Type { get; set; }

            public StringBuilder Builder
            {
                get { return builder; }
            }
        }

        #endregion

        #region Nested type: TokenType

        private enum TokenType
        {
            OpenTag,
            CloseTag,
            AttributeName,
            AttributeValue,
            Text,
        }

        #endregion
    }
}

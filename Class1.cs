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
            HtmlNode node = Parse(html).First();
            Assert.Equal("html", node.Name);
        }

        [Fact]
        public void ParseNodeBody()
        {
            const string html = "<body></body>";
            HtmlNode node = Parse(html).First();
            Assert.Equal("body", node.Name);
        }

        [Fact]
        public void Parse2Tags()
        {
            const string html = "<head></head><body></body>";
            IEnumerable<HtmlNode> nodes = Parse(html);
            HtmlNode first = nodes.First();
            HtmlNode last = nodes.Last();
            Assert.Equal("head", first.Name);
            Assert.Equal("body", last.Name);
        }

        [Fact]
        public void ParseTextAndTag()
        {
            const string html = "<<head></head>";
            IEnumerable<HtmlNode> nodes = Parse(html);
            HtmlNode first = nodes.First();
            HtmlNode last = nodes.Last();
            Assert.Equal("<", first.Name);
            Assert.Equal("head", last.Name);
        }

        [Fact]
        public void ParseNestedTags()
        {
            const string html = "<head><title></title></head>";
            IEnumerable<HtmlNode> nodes = Parse(html);
            HtmlNode first = nodes.First();
            HtmlNode last = first.Nodes.Single();
            Assert.Equal("head", first.Name);
            Assert.Equal("title", last.Name);
        }

        private static IEnumerable<HtmlNode> Parse(string html)
        {
            IEnumerable<Token> tokens = ParseTokens(html);
            return BuildTree(tokens);
        }

        private static IEnumerable<Token> ParseTokens(string html)
        {
            var context = new Context();
            var tokens = new List<Token>();
            foreach (char ch in html)
            {
                if (ch == '<')
                {
                    switch (context.State)
                    {
                        case ParseState.Default:
                        case ParseState.Text:
                            tokens.Add(context.CurrentToken);
                            context.SwitchState(ParseState.Tag, TokenType.OpenTag);
                            break;
                        case ParseState.Tag:
                            context.PreviousToken.Builder.Append(context.PreviousChar);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (ch == '>')
                {
                    switch (context.State)
                    {
                        case ParseState.Default:
                        case ParseState.Text:
                            context.CurrentToken.Builder.Append(ch);
                            break;
                        case ParseState.Tag:
                            if (context.CurrentToken.IsNotEmpty())
                                tokens.Add(context.CurrentToken);
                            context.SwitchState(ParseState.Default, TokenType.Text);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (ch == '/')
                {
                    switch (context.State)
                    {
                        case ParseState.Default:
                        case ParseState.Text:
                            context.CurrentToken.Builder.Append(ch);
                            break;
                        case ParseState.Tag:
                            context.CurrentToken.Type = TokenType.CloseTag;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    context.CurrentToken.Builder.Append(ch);
                }
                context.PreviousChar = ch;
            }

            tokens.Add(context.CurrentToken);
            return tokens.Where(x => x.IsNotEmpty());
        }

        private static IEnumerable<HtmlNode> BuildTree(IEnumerable<Token> tokens)
        {
            var parent = new HtmlNode();
            var tree = new List<HtmlNode>();
            var currentNode = parent;
            var stack = new Stack<HtmlNode>();
            foreach (Token token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.OpenTag:
                        var node = new HtmlNode
                                           {
                                               Name = token.Builder.ToString()
                                           };
                        currentNode.AddChild(node);
                        stack.Push(currentNode);
                        currentNode = node;
                        break;
                    case TokenType.CloseTag:
                        currentNode = stack.Pop();
                        break;
                    case TokenType.AttributeName:
                        break;
                    case TokenType.AttributeValue:
                        break;
                    case TokenType.Text:
                        currentNode.AddChild(new HtmlNode {Name = token.Builder.ToString()});
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return parent.Nodes;
        }

        #region Nested type: Context

        private class Context
        {
            public Context()
            {
                State = ParseState.Default;
                PreviousToken = new Token(TokenType.Text);
                CurrentToken = new Token(TokenType.Text);
            }

            public ParseState State { get; private set; }

            public Token PreviousToken { get; private set; }

            public char PreviousChar { get; set; }

            public Token CurrentToken { get; private set; }

            public void SwitchState(ParseState state, TokenType tokenType)
            {
                State = state;
                PreviousToken = CurrentToken;
                CurrentToken = new Token(tokenType);
            }
        }

        #endregion

        #region Nested type: HtmlNode

        private class HtmlNode
        {
            private readonly ICollection<HtmlNode> nodes = new List<HtmlNode>();
            
            public string Name { get; set; }

            public IEnumerable<HtmlNode> Nodes
            {
                get { return nodes; }
            }

            public void AddChild(HtmlNode node)
            {
                nodes.Add(node);
            }
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

            public bool IsNotEmpty()
            {
                return Builder.Length > 0;
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

using System;
using System.Collections.Generic;

namespace ClassLibrary3
{
    internal class TreeBuilder
    {
        public static IEnumerable<HtmlNode> BuildTree(IEnumerable<Token> tokens)
        {
            var parent = new HtmlNode();
            var tree = new List<HtmlNode>();
            HtmlNode currentNode = parent;
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
    }
}
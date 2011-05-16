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
            HtmlAttribute currentAttribute = null;
            foreach (Token token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.OpenTag:
                        var node = new HtmlNode
                                       {
                                           Name = token.Value
                                       };
                        currentNode.AddChild(node);
                        stack.Push(currentNode);
                        currentNode = node;
                        break;
                    case TokenType.CloseTag:
                        currentNode = stack.Pop();
                        break;
                    case TokenType.AttributeName:
                        currentAttribute = new HtmlAttribute
                                               {
                                                   Name = token.Value
                                               };
                        currentNode.AddAttribute(currentAttribute);
                        break;
                    case TokenType.AttributeValue:
                        currentAttribute.Value = token.Value;
                        break;
                    case TokenType.Text:
                        currentNode.AddChild(new HtmlNode {Name = token.Value});
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return parent.Nodes;
        }
    }
}
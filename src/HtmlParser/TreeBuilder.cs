namespace HtmlParser
{
    using System;
    using System.Collections.Generic;

    internal static class TreeBuilder
    {
        private static readonly IDictionary<string, HtmlElementFlag> elementsFlags =
            new Dictionary<string, HtmlElementFlag>
                {
                    {"script", HtmlElementFlag.CData},
                    {"style", HtmlElementFlag.CData},
                    {"noxhtml", HtmlElementFlag.CData},
                    {"base", HtmlElementFlag.Empty},
                    {"link", HtmlElementFlag.Empty},
                    {"meta", HtmlElementFlag.Empty},
                    {"isindex", HtmlElementFlag.Empty},
                    {"hr", HtmlElementFlag.Empty},
                    {"col", HtmlElementFlag.Empty},
                    {"img", HtmlElementFlag.Empty},
                    {"param", HtmlElementFlag.Empty},
                    {"embed", HtmlElementFlag.Empty},
                    {"frame", HtmlElementFlag.Empty},
                    {"wbr", HtmlElementFlag.Empty},
                    {"bgsound", HtmlElementFlag.Empty},
                    {"spacer", HtmlElementFlag.Empty},
                    {"keygen", HtmlElementFlag.Empty},
                    {"area", HtmlElementFlag.Empty},
                    {"input", HtmlElementFlag.Empty},
                    {"basefont", HtmlElementFlag.Empty},
                    {"form", HtmlElementFlag.CanOverlap},
                    {"option", HtmlElementFlag.Empty},
                    {"br", HtmlElementFlag.Empty},
                };

        public static HtmlNode Build(IEnumerable<Token> tokens)
        {
            var documentNode = new HtmlDocumentNode();

            HtmlNode currentNode = documentNode;
            var stack = new Stack<HtmlNode>();
            HtmlAttributeNode currentAttribute = null;
            foreach (Token token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.OpenTag:
                        var node = new HtmlElementNode(token.Value);
                        currentNode = PopCurrentNodeParentIfCurrentNodeIsEmptyTag(stack, currentNode);
                        currentNode.AppendChild(node);
                        stack.Push(currentNode);
                        currentNode = node;
                        break;
                    case TokenType.CloseTag:
                        currentNode = stack.Pop();
                        break;
                    case TokenType.AttributeName:
                        currentAttribute = new HtmlAttributeNode(token.Value);
                        currentNode.AppendAttribute(currentAttribute);
                        break;
                    case TokenType.AttributeValue:
                        currentAttribute.Value = token.Value;
                        break;
                    case TokenType.Text:
                        currentNode = PopCurrentNodeParentIfCurrentNodeIsEmptyTag(stack, currentNode);
                        currentNode.AppendChild(new HtmlTextNode(token.Value));
                        break;
                    case TokenType.Comment:
                        currentNode = PopCurrentNodeParentIfCurrentNodeIsEmptyTag(stack, currentNode);
                        currentNode.AppendChild(new HtmlCommentNode(token.Value));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return documentNode;
        }

        private static HtmlNode PopCurrentNodeParentIfCurrentNodeIsEmptyTag(Stack<HtmlNode> stack, HtmlNode currentNode)
        {
            HtmlElementFlag elementFlag;
            string key = currentNode.Name ?? string.Empty;
            elementsFlags.TryGetValue(key.ToLower(), out elementFlag);
            if (elementFlag == HtmlElementFlag.Empty)
                currentNode = stack.Pop();
            return currentNode;
        }
    }
}
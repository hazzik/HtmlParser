namespace HtmlParser
{
    using System;
    using System.Collections.Generic;

    internal class TreeBuilder
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
            var documentNode = new HtmlNode(HtmlNodeType.Element, "#document");

            HtmlNode currentNode = documentNode;
            var stack = new Stack<HtmlNode>();
            HtmlAttribute currentAttribute = null;
            foreach (Token token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.OpenTag:
                        HtmlElementFlag elementFlag;
                        elementsFlags.TryGetValue(currentNode.Name ?? string.Empty, out elementFlag);
                        if (elementFlag == HtmlElementFlag.Empty)
                            currentNode = stack.Pop();
                        var node = new HtmlNode(HtmlNodeType.Element, token.Value);
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
                        currentNode.AddChild(new HtmlNode(HtmlNodeType.Text, token.Value));
                        break;
                    case TokenType.Comment:
                        currentNode.AddChild(new HtmlNode(HtmlNodeType.Comment, token.Value));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return documentNode;
        }
    }
}
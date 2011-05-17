using System;
using System.Collections.Generic;

namespace HtmlParser
{
    internal class HtmlParser
    {
        public static IEnumerable<HtmlNode> Parse(string html)
        {
            IEnumerable<Token> tokens = TokenParser.Parse(html);
            return TreeBuilder.BuildTree(tokens);
        }
    }
}
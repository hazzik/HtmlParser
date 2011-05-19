namespace HtmlParser
{
    using System.Collections.Generic;

    public static class HtmlParser
    {
        public static HtmlNode Parse(string html)
        {
            IEnumerable<Token> tokens = new TokenParser().Parse(html);
            return TreeBuilder.Build(tokens);
        }
    }
}
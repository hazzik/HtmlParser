namespace HtmlParser
{
	using System.Collections.Generic;
    using System.Linq;

	internal static class TokenParser
    {
        public static IEnumerable<Token> Parse(string html)
        {
            var context = new Context();
            var tokens = new List<Token>();
            foreach (char ch in html)
            {
            	context.Handle(tokens, ch);
            }

			tokens.Add(context.SwitchState(TokenType.Text, new TextHandler()));
            return tokens.Where(x => x.IsNotEmpty());
        }
    }
}
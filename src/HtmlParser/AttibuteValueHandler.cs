namespace HtmlParser
{
	using System.Collections.Generic;
	using System.Linq;

	internal class AttibuteValueHandler : ParserState
	{
		private readonly IEnumerable<char> chars;

		public AttibuteValueHandler(params char[] chars)
		{
			this.chars = chars;
		}

		protected override bool HandleCore(TokenParser context, ICollection<Token> tokens, char ch)
		{
			if (ch == '>')
			{
				tokens.Add(context.SwitchState(TokenType.Text, new TextHandler()));
				return true;
			}
			if (chars.Contains(ch))
			{
				tokens.Add(context.SwitchState(TokenType.AttributeName, new AttibuteNameHandler()));
				return true;
			}
			return false;
		}
	}
}
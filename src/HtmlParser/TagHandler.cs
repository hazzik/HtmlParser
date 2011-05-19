namespace HtmlParser
{
	using System;
	using System.Collections.Generic;

	internal class TagHandler : ParserState
	{
		protected override bool HandleCore(Context context, ICollection<Token> tokens, char ch)
		{
			if (ch == '>')
			{
				Token token = context.SwitchState(TokenType.Text, new TextHandler());
				tokens.Add(token);
				return true;
			}
			if (Char.IsWhiteSpace(ch))
			{
				tokens.Add(context.SwitchState(TokenType.AttributeName, new AttibuteNameHandler()));
				return true;
			}
			return false;
		}
	}
}
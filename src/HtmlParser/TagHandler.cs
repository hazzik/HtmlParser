namespace HtmlParser
{
	using System;
	using System.Collections.Generic;

	internal class TagHandler : ParserState
	{
		protected override bool HandleCore(TokenParser context, ICollection<Token> tokens, char ch)
		{
			if (ch == '>')
			{
				Token token = context.SwitchState(TokenType.Text, new TextHandler());
				tokens.Add(token);
				if (token.Type == TokenType.OpenTag && (token.Value == "script" || token.Value == "style"))
				{
					tokens.Add(context.SwitchState(TokenType.Text, new CDataHandler(string.Format("</{0}", token.Value))));
				}
				return true;
			}
			if (ch == '/' || Char.IsWhiteSpace(ch))
			{
				var handler = new AttibuteNameHandler();
				Token token = context.SwitchState(TokenType.AttributeName, handler);
				if (token.Type == TokenType.OpenTag && (token.Value == "script" || token.Value == "style"))
				{
					handler.ReplaceNextTagOrTextTokenWithCData = string.Format("</{0}", token.Value);
				}
				tokens.Add(token);
				return true;
			}
			return false;
		}
	}
}
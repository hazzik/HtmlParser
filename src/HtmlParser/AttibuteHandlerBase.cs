namespace HtmlParser
{
	using System.Collections.Generic;

	internal class AttibuteHandlerBase : ParserState
	{
		protected internal string ReplaceNextTagOrTextTokenWithCData;

		protected override bool HandleCore(TokenParser context, ICollection<Token> tokens, char ch)
		{
			if (ch == '>')
			{
				tokens.Add(string.IsNullOrEmpty(ReplaceNextTagOrTextTokenWithCData)
				           	? context.SwitchState(TokenType.Text, new TextHandler())
				           	: context.SwitchState(TokenType.Text, new CDataHandler(ReplaceNextTagOrTextTokenWithCData)));

				return true;
			}
			return false;
		}
	}
}
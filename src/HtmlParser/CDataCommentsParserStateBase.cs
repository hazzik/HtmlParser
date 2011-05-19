namespace HtmlParser
{
	using System.Collections.Generic;

	internal abstract class CDataCommentsParserStateBase : ParserState
	{
		protected readonly string CloseToken;
		private int index;

		protected CDataCommentsParserStateBase(string closeToken)
		{
			CloseToken = closeToken;
		}

		protected override bool HandleCore(TokenParser context, ICollection<Token> tokens, char ch)
		{
			if (CloseToken[index] == ch)
			{
				index++;
				if (index >= CloseToken.Length)
				{
					OnClose(context, tokens);
				}
				return true;
			}
			if (index > 0)
			{
				context.TokenBuilder.Append(CloseToken.Substring(0, index));
				index = 0;
			}
			return false;
		}

		protected abstract void OnClose(TokenParser context, ICollection<Token> tokens);
	}
}
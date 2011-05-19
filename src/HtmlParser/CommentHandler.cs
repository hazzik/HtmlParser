namespace HtmlParser
{
	using System.Collections.Generic;

	internal class CommentHandler : ParserState
	{
		private readonly string closeToken;
		private int index;

		public CommentHandler()
		{
			closeToken = "-->";
		}

		protected override bool HandleCore(TokenParser context, ICollection<Token> tokens, char ch)
		{
			if (closeToken[index] == ch)
			{
				index++;
				if (index >= closeToken.Length)
				{
					tokens.Add(context.SwitchState(TokenType.Text, new TextHandler()));
				}
				return true;
			}
			if (ch == '-')
			{
				return false;
			}
			if (index > 0)
			{
				context.TokenBuilder.Append(closeToken.Substring(0, index));
				index = 0;
			}
			return false;
		}
	}
}
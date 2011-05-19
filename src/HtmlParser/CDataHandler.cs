namespace HtmlParser
{
	using System.Collections.Generic;

	internal class CDataHandler : ParserState
	{
		private readonly string closeToken;
		private int index;

		public CDataHandler(string closeToken)
		{
			this.closeToken = closeToken;
		}

		protected override bool HandleCore(Context context, ICollection<Token> tokens, char ch)
		{
			if (closeToken[index] == ch)
			{
				index++;
				if (index >= closeToken.Length)
				{
					tokens.Add(context.SwitchState(TokenType.CloseTag, new TagHandler()));
					context.TokenBuilder.Append(closeToken.Substring(2));
				}

				return true;
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
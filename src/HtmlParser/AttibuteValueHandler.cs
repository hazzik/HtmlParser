namespace HtmlParser
{
	using System.Collections.Generic;
	using System.Linq;

	internal class AttibuteValueHandler : AttibuteHandlerBase
	{
		private readonly IEnumerable<char> chars;

		public AttibuteValueHandler(params char[] chars)
		{
			this.chars = chars;
		}

		protected override bool HandleCore(TokenParser context, ICollection<Token> tokens, char ch)
		{
			if (chars.Contains(ch))
			{
				tokens.Add(context.SwitchState(TokenType.AttributeName,
				                               new AttibuteNameHandler
				                               	{
				                               		ReplaceNextTagOrTextTokenWithCData = ReplaceNextTagOrTextTokenWithCData
				                               	}));
				return true;
			}
			return base.HandleCore(context, tokens, ch);
		}
	}
}
namespace HtmlParser
{
	using System.Collections.Generic;

	internal class CDataHandler : CDataCommentsParserStateBase
	{
		public CDataHandler(string closeToken) : base(closeToken)
		{
		}

		protected override void OnClose(TokenParser context, ICollection<Token> tokens)
		{
			tokens.Add(context.SwitchState(TokenType.CloseTag, new TagHandler()));
			context.TokenBuilder.Append(CloseToken.Substring(2));
		}
	}
}
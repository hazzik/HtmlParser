namespace HtmlParser
{
	using System.Collections.Generic;

	internal class CommentHandler : CDataCommentsParserStateBase
	{
		public CommentHandler() : base("-->")
		{
		}

		protected override void OnClose(TokenParser context, ICollection<Token> tokens)
		{
			tokens.Add(context.SwitchState(TokenType.Text, new TextHandler()));
		}
	}
}
namespace HtmlParser
{
	using System.Collections.Generic;

	internal class TextHandler : ParserState
	{
		private State state = State.Default;

		protected override bool HandleCore(TokenParser context, ICollection<Token> tokens, char ch)
		{
			if (state == State.Default)
			{
				if (ch == '<')
				{
					state = State.WaitForTagOrComment;
					return true;
				}
				return false;
			}
			if (state == State.WaitForTagOrComment)
			{
				if (ch == '<')
				{
					context.TokenBuilder.Append('<');
					return true;
				}
				if (ch == '/')
				{
					tokens.Add(context.SwitchState(TokenType.CloseTag, new TagHandler()));
					return true;
				}
				if (ch == '!')
				{
					state = State.WaitForMinus;
					return true;
				}
				tokens.Add(context.SwitchState(TokenType.OpenTag, new TagHandler()));
				return false;
			}
			if (state == State.WaitForMinus)
			{
				if (ch == '-')
				{
					state = State.WaitForSecondMinus;
					return true;
				}
				state = State.Default;
				context.TokenBuilder.Append("<!");
				return false;
			}
			if (state == State.WaitForSecondMinus)
			{
				if (ch == '-')
				{
					tokens.Add(context.SwitchState(TokenType.Comment, new CommentHandler()));
					return true;
				}
				state = State.Default;
				context.TokenBuilder.Append("<!-");
				return false;
			}
			return false;
		}

		#region Nested type: State

		private enum State
		{
			Default,
			WaitForTagOrComment,
			WaitForMinus,
			WaitForSecondMinus
		}

		#endregion
	}
}
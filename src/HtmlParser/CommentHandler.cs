namespace HtmlParser
{
	using System.Collections.Generic;

	internal class CommentHandler : ParserState
	{
		private State state = State.Default;

		protected override bool HandleCore(Context context, ICollection<Token> tokens, char ch)
		{
			if (state == State.Default)
			{
				if (ch == '-')
				{
					state = State.WaitForSecondMinus;
					return true;
				}
				return false;
			}
			if (state == State.WaitForSecondMinus)
			{
				if (ch == '-')
				{
					state = State.WaitForGt;
					return true;
				}
				state = State.Default;
				context.TokenBuilder.Append("-");
				return false;
			}
			if (state == State.WaitForGt)
			{
				if (ch == '>')
				{
					tokens.Add(context.SwitchState(TokenType.Text, new TextHandler()));
					return true;
				}

				if (ch == '-')
				{
					return false;
				}
				state = State.Default;
				context.TokenBuilder.Append("--");
				return false;
			}
			return false;
		}

		#region Nested type: State

		private enum State
		{
			Default,
			WaitForSecondMinus,
			WaitForGt
		}

		#endregion
	}
}
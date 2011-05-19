namespace HtmlParser
{
	using System;
	using System.Collections.Generic;

	internal class AttibuteNameHandler : ParserState
	{
		private State state = State.Default;

		protected override bool HandleCore(TokenParser context, ICollection<Token> tokens, char ch)
		{
			if (ch == '>')
			{
				tokens.Add(context.SwitchState(TokenType.Text, new TextHandler()));
				return true;
			}

			if (state == State.AttributeValueBegin)
			{
				if (ch == '"' || ch == '\'')
				{
					tokens.Add(context.SwitchState(TokenType.AttributeValue, new AttibuteValueHandler(ch)));
					return true;
				}

				if (!Char.IsWhiteSpace(ch))
				{
					tokens.Add(context.SwitchState(TokenType.AttributeValue,
					                               new AttibuteValueHandler(' ', '\t', '\r', '\x00a0', '\x0085')));
					return false;
				}

				return true;
			}

			if (ch == '=')
			{
				state = State.AttributeValueBegin;
				return true;
			}

			if (ch == '/' || Char.IsWhiteSpace(ch))
			{
				tokens.Add(context.SwitchState(TokenType.AttributeName, new AttibuteNameHandler()));
				return true;
			}
			return false;
		}

		#region Nested type: State

		private enum State
		{
			Default,
			AttributeValueBegin,
		}

		#endregion
	}
}
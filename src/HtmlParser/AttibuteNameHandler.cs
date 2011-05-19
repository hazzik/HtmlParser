namespace HtmlParser
{
	using System;
	using System.Collections.Generic;

	internal class AttibuteNameHandler : AttibuteHandlerBase
	{
		private State state = State.Default;

		protected override bool HandleCore(TokenParser context, ICollection<Token> tokens, char ch)
		{
			if (state == State.AttributeValueBegin)
			{
				if (ch == '"' || ch == '\'')
				{
					tokens.Add(context.SwitchState(TokenType.AttributeValue, new AttibuteValueHandler(ch)
					                                                         	{
					                                                         		ReplaceNextTagOrTextTokenWithCData =
					                                                         			ReplaceNextTagOrTextTokenWithCData
					                                                         	}));
					return true;
				}

				if (!Char.IsWhiteSpace(ch))
				{
					tokens.Add(context.SwitchState(TokenType.AttributeValue,
					                               new AttibuteValueHandler(' ', '\t', '\r', '\x00a0', '\x0085')
					                               	{
					                               		ReplaceNextTagOrTextTokenWithCData = ReplaceNextTagOrTextTokenWithCData
					                               	}));
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
				tokens.Add(context.SwitchState(TokenType.AttributeName, new AttibuteNameHandler
				                                                        	{
				                                                        		ReplaceNextTagOrTextTokenWithCData =
				                                                        			ReplaceNextTagOrTextTokenWithCData
				                                                        	}));
				return true;
			}
			return base.HandleCore(context, tokens, ch);
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
using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlParser
{
    internal class TokenParser
    {
        public static IEnumerable<Token> Parse(string html)
        {
            var context = new Context();
            var tokens = new List<Token>();
            foreach (char ch in html)
            {
                if (HandleCharacter(tokens, context, ch) == false)
                {
                    context.CurrentToken.Builder.Append(ch);
                }
                context.PreviousChar = ch;
            }

            tokens.Add(context.CurrentToken);
            return tokens.Where(x => x.IsNotEmpty());
        }

        private static bool HandleCharacter(ICollection<Token> tokens, Context context, char ch)
        {
            if (ch == '<')
            {
                return HandleLt(tokens, context);
            }
            if (ch == '>')
            {
                return HandleGt(tokens, context);
            }
            if (ch == '/')
            {
                return HandleSlash(tokens, context);
            }
            if (ch == '=')
            {
                return HandleEquals(tokens, context);
            }
            if (ch == '"')
            {
                return HandleDoubleQuote(tokens, context);
            }
            if (ch == '\'')
            {
                return HandleSingleQuote(tokens, context);
            }
            if (char.IsWhiteSpace(ch))
            {
                return HandleWhitespace(tokens, context);
            }
            return HandleAny(context);
        }

        private static bool HandleLt(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.Default || context.State == ParseState.Text)
            {
                tokens.Add(context.CurrentToken);
                context.SwitchState(ParseState.Tag, TokenType.OpenTag);
                return true;
            }
            if (context.State == ParseState.Tag)
            {
                context.PreviousToken.Builder.Append(context.PreviousChar);
                return true;
            }
            if (context.State == ParseState.AttibuteValueBegin)
                context.SwitchState(ParseState.AttibuteValue, TokenType.AttributeValue);
            return false;
        }

        private static bool HandleGt(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.Tag || context.State == ParseState.AttibuteName || context.State == ParseState.AttibuteValueBegin || context.State == ParseState.AttibuteValue)
            {
                tokens.Add(context.CurrentToken);
                context.SwitchState(ParseState.Default, TokenType.Text);
                return true;
            }
            return false;
        }

        private static bool HandleSlash(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.Tag)
            {
                context.CurrentToken.Type = TokenType.CloseTag;
                return true;
            }
            if (context.State == ParseState.AttibuteName)
            {
                tokens.Add(context.CurrentToken);
                context.SwitchState(ParseState.AttibuteName, TokenType.AttributeName);
                return true;
            }
            if (context.State == ParseState.AttibuteValueBegin)
                context.SwitchState(ParseState.AttibuteValue, TokenType.AttributeValue);
            return false;
        }

        private static bool HandleEquals(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.AttibuteName)
            {
                tokens.Add(context.CurrentToken);
                context.SwitchState(ParseState.AttibuteValueBegin, TokenType.AttributeValue);
                return true;
            }
            if (context.State == ParseState.AttibuteValueBegin)
                context.SwitchState(ParseState.AttibuteValue, TokenType.AttributeValue);
            return false;
        }

        private static bool HandleAny(Context context)
        {
            if (context.State == ParseState.AttibuteValueBegin)
                context.SwitchState(ParseState.AttibuteValue, TokenType.AttributeValue);
            return false;
        }

        private static bool HandleDoubleQuote(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.AttibuteValueBegin)
            {
                context.SwitchState(ParseState.DoubleQuotedAttibuteValue, TokenType.AttributeValue);
                return true;
            }
            if (context.State == ParseState.DoubleQuotedAttibuteValue)
            {
                tokens.Add(context.CurrentToken);
                context.SwitchState(ParseState.AttibuteName, TokenType.AttributeName);
                return true;
            }
            return false;
        }

        private static bool HandleSingleQuote(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.AttibuteValueBegin)
            {
                context.SwitchState(ParseState.SingleQuotedAttibuteValue, TokenType.AttributeValue);
                return true;
            }
            if (context.State == ParseState.SingleQuotedAttibuteValue)
            {
                tokens.Add(context.CurrentToken);
                context.SwitchState(ParseState.AttibuteName, TokenType.AttributeName);
                return true;
            }
            return false;
        }

        private static bool HandleWhitespace(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.Tag ||
                context.State == ParseState.AttibuteName ||
                context.State == ParseState.AttibuteValue)
            {
                tokens.Add(context.CurrentToken);
                context.SwitchState(ParseState.AttibuteName, TokenType.AttributeName);
                return true;
            }
            return context.State == ParseState.AttibuteValueBegin;
        }
    }
}

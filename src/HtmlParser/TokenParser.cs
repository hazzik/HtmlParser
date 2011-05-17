namespace HtmlParser
{
    using System.Collections.Generic;
    using System.Linq;

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
                    context.TokenBuilder.Append(ch);
                }
            }

            tokens.Add(context.SwitchState(ParseState.Text, TokenType.Text));
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
            if (ch == '-')
            {
                return HandleMinus(tokens, context);
            }
            if (char.IsWhiteSpace(ch))
            {
                return HandleWhitespace(tokens, context);
            }
            return HandleAny(tokens, context);
        }

        private static bool HandleMinus(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.WaitForSecondCloseMinus)
            {
                context.SetState(ParseState.WaitForGt);
                return true;
            }
            if (context.State == ParseState.Comment)
            {
                context.SetState(ParseState.WaitForSecondCloseMinus);
                return true;
            }
            if (context.State == ParseState.WhaitForTagOrComment)
            {
                context.SetState(ParseState.WaitForSecondOpenMinus);
                return true;
            }
            if (context.State == ParseState.WaitForSecondOpenMinus)
            {
                tokens.Add(context.SwitchState(ParseState.Comment, TokenType.Comment));
                return true;
            }
            return false;
        }

        private static bool HandleLt(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.WhaitForTagOrComment)
            {
                context.TokenBuilder.Append('<');
                return true;
            }
            if (context.State == ParseState.Text)
            {
                context.SetState(ParseState.WhaitForTagOrComment);
                return true;
            }
            if (context.State == ParseState.AttibuteValueBegin)
            {
                tokens.Add(context.SwitchState(ParseState.AttibuteValue, TokenType.AttributeValue));
            }
            return false;
        }

        private static bool HandleGt(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.Tag ||
                context.State == ParseState.AttibuteName ||
                context.State == ParseState.AttibuteValueBegin ||
                context.State == ParseState.AttibuteValue ||
                context.State == ParseState.WaitForGt)
            {
                tokens.Add(context.SwitchState(ParseState.Text, TokenType.Text));
                return true;
            }
            return false;
        }

        private static bool HandleSlash(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.WhaitForTagOrComment)
            {
                tokens.Add(context.SwitchState(ParseState.Tag, TokenType.CloseTag));
                return true;
            }
            if (context.State == ParseState.AttibuteName)
            {
                tokens.Add(context.SwitchState(ParseState.AttibuteName, TokenType.AttributeName));
                return true;
            }
            if (context.State == ParseState.AttibuteValueBegin)
            {
                tokens.Add(context.SwitchState(ParseState.AttibuteValue, TokenType.AttributeValue));
            }
            return false;
        }

        private static bool HandleEquals(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.AttibuteName)
            {
                context.SetState(ParseState.AttibuteValueBegin);
                return true;
            }
            if (context.State == ParseState.AttibuteValueBegin)
            {
                tokens.Add(context.SwitchState(ParseState.AttibuteValue, TokenType.AttributeValue));
            }
            return false;
        }

        private static bool HandleAny(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.AttibuteValueBegin)
            {
                tokens.Add(context.SwitchState(ParseState.AttibuteValue, TokenType.AttributeValue));
            }
            if (context.State == ParseState.WhaitForTagOrComment)
            {
                tokens.Add(context.SwitchState(ParseState.Tag, TokenType.OpenTag));
            }
            if (context.State == ParseState.WaitForSecondOpenMinus)
            {
                context.SetState(ParseState.Text);
                context.TokenBuilder.Append("<-");
            }
            if (context.State == ParseState.WaitForSecondCloseMinus)
            {
                context.SetState(ParseState.Comment);
                context.TokenBuilder.Append("-");
            }
            if (context.State == ParseState.WaitForGt)
            {
                context.SetState(ParseState.Comment);
                context.TokenBuilder.Append("--");
            }
            return false;
        }

        private static bool HandleDoubleQuote(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.AttibuteValueBegin)
            {
                tokens.Add(context.SwitchState(ParseState.DoubleQuotedAttibuteValue, TokenType.AttributeValue));
                return true;
            }
            if (context.State == ParseState.DoubleQuotedAttibuteValue)
            {
                tokens.Add(context.SwitchState(ParseState.AttibuteName, TokenType.AttributeName));
                return true;
            }
            return false;
        }

        private static bool HandleSingleQuote(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.AttibuteValueBegin)
            {
                tokens.Add(context.SwitchState(ParseState.SingleQuotedAttibuteValue, TokenType.AttributeValue));
                return true;
            }
            if (context.State == ParseState.SingleQuotedAttibuteValue)
            {
                tokens.Add(context.SwitchState(ParseState.AttibuteName, TokenType.AttributeName));
                return true;
            }
            return false;
        }

        private static bool HandleWhitespace(ICollection<Token> tokens, Context context)
        {
            if (context.State == ParseState.WaitForSecondOpenMinus)
            {
                context.SetState(ParseState.Text);
                context.TokenBuilder.Append("<-");
            }
            if (context.State == ParseState.WaitForSecondCloseMinus)
            {
                context.SetState(ParseState.Comment);
                context.TokenBuilder.Append("-");
            }
            if (context.State == ParseState.WaitForGt)
            {
                context.SetState(ParseState.Comment);
                context.TokenBuilder.Append("--");
            }
            if (context.State == ParseState.Tag ||
                context.State == ParseState.AttibuteName ||
                context.State == ParseState.AttibuteValue)
            {
                tokens.Add(context.SwitchState(ParseState.AttibuteName, TokenType.AttributeName));
                return true;
            }
            return context.State == ParseState.AttibuteValueBegin;
        }
    }
}
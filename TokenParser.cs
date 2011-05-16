using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary3
{
    internal class TokenParser
    {
        public static IEnumerable<Token> Parse(string html)
        {
            var context = new Context();
            var tokens = new List<Token>();
            foreach (char ch in html)
            {
                if (ch == '<')
                {
                    switch (context.State)
                    {
                        case ParseState.Default:
                        case ParseState.Text:
                            tokens.Add(context.CurrentToken);
                            context.SwitchState(ParseState.Tag, TokenType.OpenTag);
                            break;
                        case ParseState.Tag:
                            context.PreviousToken.Builder.Append(context.PreviousChar);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (ch == '>')
                {
                    switch (context.State)
                    {
                        case ParseState.Default:
                        case ParseState.Text:
                            context.CurrentToken.Builder.Append(ch);
                            break;
                        case ParseState.Tag:
                        case ParseState.AttibuteName:
                            tokens.Add(context.CurrentToken);
                            context.SwitchState(ParseState.Default, TokenType.Text);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (ch == '/')
                {
                    switch (context.State)
                    {
                        case ParseState.Default:
                        case ParseState.Text:
                            context.CurrentToken.Builder.Append(ch);
                            break;
                        case ParseState.Tag:
                            context.CurrentToken.Type = TokenType.CloseTag;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (char.IsWhiteSpace(ch))
                {
                    switch (context.State)
                    {
                        case ParseState.Default:
                        case ParseState.Text:
                            context.CurrentToken.Builder.Append(ch);
                            break;
                        case ParseState.Tag:
                        case ParseState.AttibuteName:
                            tokens.Add(context.CurrentToken);
                            context.SwitchState(ParseState.AttibuteName, TokenType.AttributeName);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    context.CurrentToken.Builder.Append(ch);
                }
                context.PreviousChar = ch;
            }

            tokens.Add(context.CurrentToken);
            return tokens.Where(x => x.IsNotEmpty());
        }
    }
}

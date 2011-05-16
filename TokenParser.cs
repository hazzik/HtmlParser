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
                        case ParseState.AttibuteName:
                        case ParseState.DoubleQuotedAttibuteValue:
                        case ParseState.SingleQuotedAttibuteValue:
                            context.CurrentToken.Builder.Append(ch);
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
                        case ParseState.DoubleQuotedAttibuteValue:
                        case ParseState.SingleQuotedAttibuteValue:
                            context.CurrentToken.Builder.Append(ch);
                            break;
                        case ParseState.Tag:
                        case ParseState.AttibuteName:
                        case ParseState.AttibuteValueBegin:
                        case ParseState.AttibuteValue:
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
                        case ParseState.AttibuteValue:
                        case ParseState.DoubleQuotedAttibuteValue:
                        case ParseState.SingleQuotedAttibuteValue:
                            context.CurrentToken.Builder.Append(ch);
                            break;
                        case ParseState.Tag:
                            context.CurrentToken.Type = TokenType.CloseTag;
                            break;
                        case ParseState.AttibuteName:
                            tokens.Add(context.CurrentToken);
                            context.SwitchState(ParseState.AttibuteName, TokenType.AttributeName);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (ch == '=')
                {
                    switch (context.State)
                    {
                        case ParseState.AttibuteValueBegin:
                            context.SwitchState(ParseState.AttibuteValue, TokenType.AttributeValue);
                            context.CurrentToken.Builder.Append(ch);
                            break;
                        case ParseState.Default:
                        case ParseState.Text:
                        case ParseState.Tag:
                        case ParseState.DoubleQuotedAttibuteValue:
                        case ParseState.SingleQuotedAttibuteValue:
                            context.CurrentToken.Builder.Append(ch);
                            break;
                        case ParseState.AttibuteName:
                            tokens.Add(context.CurrentToken);
                            context.SwitchState(ParseState.AttibuteValueBegin, TokenType.AttributeValue);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (ch == '"')
                {
                    switch (context.State)
                    {
                        case ParseState.Default:
                        case ParseState.Text:
                        case ParseState.Tag:
                        case ParseState.AttibuteName:
                        case ParseState.AttibuteValue:
                        case ParseState.SingleQuotedAttibuteValue:
                            context.CurrentToken.Builder.Append(ch);
                            break;
                        case ParseState.AttibuteValueBegin:
                            context.SwitchState(ParseState.DoubleQuotedAttibuteValue, TokenType.AttributeValue);
                            break;
                        case ParseState.DoubleQuotedAttibuteValue:
                            tokens.Add(context.CurrentToken);
                            context.SwitchState(ParseState.AttibuteName, TokenType.AttributeName);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (ch == '\'')
                {
                    switch (context.State)
                    {
                        case ParseState.Default:
                        case ParseState.Text:
                        case ParseState.Tag:
                        case ParseState.AttibuteName:
                        case ParseState.AttibuteValue:
                        case ParseState.DoubleQuotedAttibuteValue:
                            context.CurrentToken.Builder.Append(ch);
                            break;
                        case ParseState.AttibuteValueBegin:
                            context.SwitchState(ParseState.SingleQuotedAttibuteValue, TokenType.AttributeValue);
                            break;
                        case ParseState.SingleQuotedAttibuteValue:
                            tokens.Add(context.CurrentToken);
                            context.SwitchState(ParseState.AttibuteName, TokenType.AttributeName);
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
                        case ParseState.DoubleQuotedAttibuteValue:
                        case ParseState.SingleQuotedAttibuteValue:
                            context.CurrentToken.Builder.Append(ch);
                            break;
                        case ParseState.Tag:
                        case ParseState.AttibuteName:
                        case ParseState.AttibuteValue:
                            tokens.Add(context.CurrentToken);
                            context.SwitchState(ParseState.AttibuteName, TokenType.AttributeName);
                            break;
                        case ParseState.AttibuteValueBegin:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    if (context.State == ParseState.AttibuteValueBegin)
                    {
                        context.SwitchState(ParseState.AttibuteValue, TokenType.AttributeValue);
                    }
                    context.CurrentToken.Builder.Append(ch);
                }
                context.PreviousChar = ch;
            }

            tokens.Add(context.CurrentToken);
            return tokens.Where(x => x.IsNotEmpty());
        }
    }
}

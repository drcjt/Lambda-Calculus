using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public class Lexer
    {
        private String _text;
        private int _nextpos = 0;

        public Token CurrentToken;
        private char? _nextchar;

        public Lexer(String text)
        {
            _text = text;
            Advance();
            NextToken();
        }

        private void Advance()
        {
            if (_nextpos < _text.Length)
            {
                _nextchar = _text[_nextpos];
            }
            else
            {
                _nextchar = null;
            }

            _nextpos++;
        }

        public Token NextToken()
        {
            while (_nextchar != null && char.IsWhiteSpace(_nextchar.Value))
            {
                Advance();

                if (_nextchar == '{')
                {
                    do
                    {
                        Advance();
                    } while (_nextchar != null && _nextchar != '}');
                    Advance();
                }
            }

            if (_nextchar == null)
            {
                CurrentToken = new Token(Symbol.EOF);
            }
            else
            {
                if (char.IsNumber(_nextchar.Value))
                {
                    var value = 0;
                    do
                    {
                        value = value * 10 + (_nextchar.Value - '0');
                        Advance();
                    } while (_nextchar != null && char.IsNumber(_nextchar.Value));
                    CurrentToken = new Token(Symbol.Number, value);
                }
                else if (char.IsLetter(_nextchar.Value))
                {
                    var identifier = "";
                    do
                    {
                        identifier += _nextchar.Value;
                        Advance();
                    } while (_nextchar != null && char.IsLetter(_nextchar.Value));

                    if (identifier.ToUpper() == "LET")
                    {
                        CurrentToken = new Token(Symbol.Let);
                    }
                    else
                    {
                        CurrentToken = new Token(Symbol.Identifier, identifier);
                    }
                }
                else
                {
                    switch (_nextchar.Value)
                    {
                        case ';': Advance();  CurrentToken = new Token(Symbol.Semi); break;
                        case '(': Advance();  CurrentToken = new Token(Symbol.LParen); break;
                        case ')': Advance();  CurrentToken = new Token(Symbol.RParen); break;
                        case '\\': Advance(); CurrentToken = new Token(Symbol.Lambda); break;
                        case '.':  Advance();  CurrentToken = new Token(Symbol.Period); break;
                        case '=': Advance();  CurrentToken = new Token(Symbol.Equals); break;
                        case '+': Advance();  CurrentToken = new Token(Symbol.Plus); break;
                        case '-': Advance();  CurrentToken = new Token(Symbol.Minus); break;
                        default: Advance();  CurrentToken = new Token(Symbol.Unknown); break;
                    }
                }
            }

            return CurrentToken;
        }
    }
}

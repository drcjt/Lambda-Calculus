using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public class Parser
    {
        private Lexer _lexer;

        public void Match(Symbol symbol)
        {
            var token = _lexer.CurrentToken;

            _lexer.NextToken();

            if (token.Symbol != symbol)
            {
                var expected = new Token(symbol);
                throw new Exception(string.Format("Syntax error: Expected {0} found {1}", expected.Symbol, token.Print()));
            }
        }

        public Expression Parse(Lexer lexer)
        {
            Expression expression;
            _lexer = lexer;

            var token = _lexer.CurrentToken;

            if (token.Symbol == Symbol.Let)
            {
                Match(Symbol.Let);
                token = _lexer.CurrentToken;
                Match(Symbol.Identifier);
                var name = token.Name;
                Match(Symbol.Equals);
                var body = ParseExpr();
                expression = new Binding(name, body);
            }
            else
            {
                expression = ParseExpr();
            }
            return expression;
        }

        public Expression ParseExpr()
        {
            var expr = ParseTerm();

            var token = _lexer.CurrentToken;
            while (token.Symbol == Symbol.Identifier ||
                   token.Symbol == Symbol.Number ||
                   token.Symbol == Symbol.Lambda ||
                   token.Symbol == Symbol.LParen)
            {
                var arg = ParseTerm();

                expr = new Application(expr, arg);

                token = _lexer.CurrentToken;
            }

            return expr;
        }

        public Expression ParseTerm()
        {
            var token = _lexer.CurrentToken;

            if (token.Symbol == Symbol.Identifier)
            {
                Match(Symbol.Identifier);
                return new Variable(token.Name);
            }
            else if (token.Symbol == Symbol.Number)
            {
                Match(Symbol.Number);
                return new Numbers(token.Value);
            }
            else if (token.Symbol == Symbol.Plus)
            {
                Match(Symbol.Plus);
                return new Primitive("+", 2);
            }
            else if (token.Symbol == Symbol.Minus)
            {
                Match(Symbol.Minus);
                return new Primitive("-", 2);
            }
            else if (token.Symbol == Symbol.Lambda)
            {
                Match(Symbol.Lambda);
                token = _lexer.CurrentToken;
                var var = token.Name;
                Match(Symbol.Identifier);
                Match(Symbol.Period);
                var expr = ParseExpr();
                return new Abstraction(var, expr);
            }
            else if (token.Symbol == Symbol.LParen)
            {
                Match(Symbol.LParen);
                var expr = ParseExpr();
                Match(Symbol.RParen);
                return expr;
            }

            return null;
        }
    }
}

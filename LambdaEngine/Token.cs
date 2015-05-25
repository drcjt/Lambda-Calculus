using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public class Token
    {
        public String Name { get; private set; }
        public int Value { get; private set; }
        public Symbol Symbol { get; private set; }

        public Token(Symbol symbol)
        {
            Symbol = symbol;
        }

        public Token(Symbol token, String name) : this(token)
        {
            Name = name;
        }

        public Token(Symbol token, int value) : this(token)
        {
            Value = value;
        }

        public string Print()
        {
            switch (Symbol)
            {
                case Symbol.Identifier:     return "Identifier";
                case Symbol.Number:         return "Number";
                case Symbol.Lambda:         return "Lambda";
                case Symbol.Period:         return "Period";
                case Symbol.LParen:         return "Left bracket";
                case Symbol.RParen:         return "Right bracket";
                case Symbol.Semi:           return "Semicolon";
                case Symbol.Equals:         return "Equals";
                case Symbol.Let:            return "Let";
                case Symbol.Plus:           return "Plus";
                case Symbol.Minus:          return "Minus";
                case Symbol.EOF:            return "End of file";
                case Symbol.Unknown:        return "Unknown";
                default:                    return "";
            }
        }
    }
}

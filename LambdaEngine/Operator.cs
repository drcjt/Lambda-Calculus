using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public class Operator : Expression
    {
        private string _name;
        private int _arity;
        private Expression _arg = null;

        public Operator(string name, int arity)
        {
            _name = name;
            _arity = arity;
        }

        public override string Print(Environment rho)
        {
            return "<Partially applied operator>";
        }

        public override Expression Apply(Expression expr)
        {
            if (_arg == null)
            {
                _arg = expr;
                return this;
            }
            else
            {
                if (_name == "+")
                {
                    var a = _arg.Evaluate(null);
                    var b = expr.Evaluate(null);

                    return new Numbers((a as Numbers).Value + (b as Numbers).Value);
                }
                else if (_name == "-")
                {
                    var a = _arg.Evaluate(null);
                    var b = expr.Evaluate(null);

                    return new Numbers((a as Numbers).Value - (b as Numbers).Value);
                }
                else
                {
                    return null;
                }
            }
        }

        public override Expression Evaluate(Environment rho)
        {
            return this;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public class Abstraction : Expression
    {
        private String _varname;
        private Expression _expr;

        public Abstraction(String varname, Expression expr)
        {
            _varname = varname;
            _expr = expr;
        }

        public override string Print(Environment rho)
        {
            Environment newrho = new Environment(_varname, new PrintVar(_varname), rho);
            return string.Format("(\\{0}.{1})", _varname, _expr.Print(newrho));
        }

        public override Expression Evaluate(Environment rho)
        {
            return new Closure(_expr, _varname, rho);
        }
    }
}

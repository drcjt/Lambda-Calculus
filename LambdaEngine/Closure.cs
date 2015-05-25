using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public class Closure : Expression
    {
        private Expression _expr;
        private String _var;
        private Environment _rho;

        public Closure(Expression expr, String var, Environment rho)
        {
            _expr = expr;
            _var = var;
            _rho = rho;
        }

        public override Expression Evaluate(Environment rho)
        {
            return this;
        }

        public override Expression Apply(Expression expr)
        {
            return _expr.Evaluate(new Environment(_var, expr, _rho));
        }

        public override string Print(Environment rho)
        {
            return string.Format("(\\{0}.{1})", _var, _expr.Print(new Environment(_var, new PrintVar(_var), _rho)));
        }
    }
}

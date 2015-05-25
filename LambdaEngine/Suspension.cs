using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public class Suspension : Expression
    {
        private Expression _expr;
        private Environment _rho;

        public Suspension(Expression expr, Environment rho)
        {
            _expr = expr;
            _rho = rho;
        }

        public override Expression Evaluate(Environment rho)
        {
            return _expr.Evaluate(_rho);
        }

        public override string Print(Environment rho)
        {
            return _expr.Print(_rho);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public class PrintVar : Expression
    {
        String _var;

        public PrintVar(String var)
        {
            _var = var;
        }

        public override string Print(Environment rho)
        {
            return _var;
        }

        public override Expression Evaluate(Environment rho)
        {
            return this;
        }
    }
}

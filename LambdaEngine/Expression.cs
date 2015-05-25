using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public abstract class Expression
    {
        public virtual string Print(Environment rho)
        {
            return "Error cannot print expression";
        }
        public virtual Expression Evaluate(Environment rho)
        {
            throw new Exception("Cannot evaluate expression");
        }

        public virtual Expression Apply(Expression expr)
        {
            throw new Exception("Cannot apply expression");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public class Numbers : Expression
    {
        public  int Value { get; private set; }

        public Numbers(int value)
        {
            Value = value;
        }

        public override string Print(Environment rho)
        {
            return Value.ToString();
        }

        public override Expression Evaluate(Environment rho)
        {
            return this;
        }
    }
}

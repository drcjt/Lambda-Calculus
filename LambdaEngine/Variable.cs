using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public class Variable : Expression
    {
        public string Name { get; private set; }

        public Variable(string name)
        {
            Name = name;
        }

        public override string Print(Environment rho)
        {
            return Name;
            //return Evaluate(rho).Print(rho);
        }

        public override Expression Evaluate(Environment rho)
        {
            if (rho == null)
            {
                throw new Exception(string.Format("Error - unbound variable, {0}", Name));
            }

            var varexp = rho.Lookup(Name);
            return varexp.Evaluate(rho);
        }
    }
}

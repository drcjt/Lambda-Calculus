using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public class Primitive : Expression
    {
        private string _name;
        private int _arity;

        public Primitive(string name, int arity)
        {
            _name = name;
            _arity = arity;
        }

        public override string Print(Environment rho)
        {
            return string.Format("<{0}, {1}>", _name, _arity);
        }

        public override Expression Evaluate(Environment rho)
        {
            return new Operator(_name, _arity);
        }
    }
}

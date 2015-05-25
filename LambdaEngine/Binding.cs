using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public class Binding : Expression
    {
        public string Name { get; private set; }
        private Expression _body;

        public Binding(String name, Expression body)
        {
            Name = name;
            _body = body;
        }

        public override string Print(Environment rho)
        {
            return string.Format("<{0},{1}>", Name, _body.Print(rho));
        }

        public override Expression Evaluate(Environment rho)
        {
            rho.Set(Name, _body);
            return this;
        }
    }
}

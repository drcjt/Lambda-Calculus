using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public class Application : Expression
    {
        private Expression _fun;
        private Expression _arg;

        public Application(Expression fun, Expression arg)
        {
            _fun = fun;
            _arg = arg;
        }

        public override string Print(Environment rho)
        {
            return string.Format("({0} {1})", _fun.Print(rho), _arg.Print(rho));
        }

        public override Expression Evaluate(Environment rho)
        {
            var susp = new Suspension(_arg, rho);
            var newfun = _fun.Evaluate(rho);
            return newfun.Apply(susp);
        }
    }
}

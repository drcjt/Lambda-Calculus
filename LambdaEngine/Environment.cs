using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public class Environment
    {
        private string _name;
        private Expression _value;

        private Environment _next;

        public Environment()
        {
            _name = "";
            _value = null;
            _next = null;
        }

        public Environment(string name, Expression value) : this(name, value, null)
        {
        }

        public Environment(String name, Expression value, Environment env)
        {
            _name = name;
            _value = value;
            _next = env;
        }

        public void Set(String name, Expression value)
        {
            Environment temp = new Environment(_name, _value, _next);
            _name = name;
            _value = value;
            _next = temp;
        }

        public Environment Extend(Environment rho)
        {
            return new Environment(_name, _value, _next != null ? _next.Extend(rho) : rho);
        }

        public Expression Lookup(string name)
        {
            if (_name == name)
            {
                return _value;
            }
            else if (_next != null)
            {
                return _next.Lookup(name);
            }
            else
            {
                return null;
            }
        }

        public void Print(IPrinter printer, Environment rho)
        {
            if (!string.IsNullOrEmpty(_name))
            {
                printer.PrintLn(string.Format("{0} = {1}", _name, _value.Print(rho)));
            }
            if (_next != null)
            {
                _next.Print(printer, rho);
            }
        }
    }
}

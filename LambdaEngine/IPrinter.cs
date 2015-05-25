using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public interface IPrinter
    {
        void Print(String text);
        void PrintLn(String text = null);
    }
}

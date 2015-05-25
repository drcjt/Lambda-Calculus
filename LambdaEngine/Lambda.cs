using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LambdaEngine
{
    public class Lambda
    {
        private Environment _topLevel;
        private IPrinter _printer;

        public Lambda(IPrinter printer)
        {
            _printer = printer;

            _topLevel = new Environment();
        }

        public void LoadPrelude(string prelude)
        {
            // read and load prelude
            try
            {
                var function = 0;
                using (var reader = new StringReader(prelude))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var lexer = new Lexer(line);
                        var parser = new Parser();
                        var expr = parser.Parse(lexer);

                        if (expr != null)
                        {
                            expr.Evaluate(_topLevel);

                            function++;

                            var bindingName = (expr as Binding).Name;

                            _printer.Print(bindingName);

                            if (function > 4)
                            {
                                _printer.PrintLn();
                                _printer.Print("\t");
                                function = 0;
                            }
                            else
                            {
                                _printer.Print(", ");
                            }
                        }
                        line = reader.ReadLine();
                    }
                }

                _printer.PrintLn();
                _printer.Print("Prelude read ok");
            }
            catch (Exception ex)
            {
                _printer.PrintLn();
                _printer.Print("Something went wrong when parsing the prelude");
            }

            _printer.PrintLn();
        }

        public void EvaluateExpression(string expression)
        {
            if (expression.ToUpper() == "PRINTBINDINGS")
            {
                // Print top level bindings
                _topLevel.Print(_printer, _topLevel);
            }
            else
            {
                try
                {
                    var lexer = new Lexer(expression);
                    var parser = new Parser();
                    var parsedExpr = parser.Parse(lexer);

                    var result = parsedExpr.Evaluate(_topLevel);
                    _printer.PrintLn(result.Print(_topLevel));
                }
                catch (Exception ex)
                {
                    _printer.PrintLn(ex.Message);
                }
            }
        }
    }
}

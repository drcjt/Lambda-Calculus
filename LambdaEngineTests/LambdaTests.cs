using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LambdaEngine;

using NUnit.Framework;
using Moq;

namespace LambdaEngineTests
{
    [TestFixture]
    class LambdaTests
    {
        [Test]
        public void EvaluateSimpleApplication()
        {
            var printer = new Mock<IPrinter>();
            printer.Setup(p => p.PrintLn("12"));


            var lambda = new Lambda(printer.Object);
            lambda.EvaluateExpression("(\\x.x) 12");

            printer.Verify();
        }
    }
}

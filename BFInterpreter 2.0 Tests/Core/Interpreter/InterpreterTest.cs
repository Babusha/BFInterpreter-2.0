using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFInterpreter_2._0.Core.Code;
using BFInterpreter_2._0.Core.Runtime.InputOutput;
using BFInterpreter_2._0.Core.Runtime.Interpreter;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BFInterpreter_2._0_Tests
{
    [TestFixture]
    public class InterpreterTest
    {
        [Test]
        public void Run_ProgramForHelloWorld()
        {
            var syntaxAnalyzer =
                Mock.Of<ISyntaxAnalyzer>(
                    property =>
                        property.Code ==
                            "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.+++++++++++++++++++++++++++++.+++++++..+++.-------------------------------------------------------------------------------.+++++++++++++++++++++++++++++++++++++++++++++++++++++++.++++++++++++++++++++++++.+++.------.--------.-------------------------------------------------------------------.-----------------------."
                                .ToCharArray());

            var code = new Code(syntaxAnalyzer);

            string expectOutput = String.Empty;
            var inputOutput = new Mock<IInputOutput>() {CallBase = true};
            inputOutput.Setup(method => method.Output(It.IsAny<char>())).Callback(delegate(char character)
            {
                expectOutput += character.ToString();
            });

            Assert.Equals(expectOutput, "Hello World!");
        }
    }
}

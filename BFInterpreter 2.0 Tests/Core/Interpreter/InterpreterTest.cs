using System;
using BFInterpreter_2._0.Core.Code;
using BFInterpreter_2._0.Core.Runtime.InputOutput;
using Moq;
using NUnit.Framework;

namespace BFInterpreter_2._0_Tests.Core.Interpreter
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

            var code = new _0.Core.Code.Code(syntaxAnalyzer);

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

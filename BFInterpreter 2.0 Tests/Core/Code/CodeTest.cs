using System;
using BFInterpreter_2._0.Core.Code;
using BFInterpreter_2._0.Core.File;
using Moq;
using NUnit.Framework;

namespace BFInterpreter_2._0_Tests.Core.Code
{
    [TestFixture]
    public class CodeTest
    {

        [Test]
        public void Constructor_PassMessedCode_NoExceptions()
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(method => method.Text()).Returns("lk34mocmt++ -=-=-= -fzfsf");

            var syntaxAnalyzer = new SyntaxAnalyzer(fileReader.Object);
            var code = new _0.Core.Code.Code(syntaxAnalyzer);
            bool notCleanedCode = true;
            foreach (var command in code.Commands)
            {
                notCleanedCode = command != '+' &&
                                 command != '-' &&
                                 command != '>' &&
                                 command != '<' &&
                                 command != '.' &&
                                 command != ',' &&
                                 command != '[' &&
                                 command != ']';
            }
            if (notCleanedCode || code.Commands.Count.Equals(0))
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }
        }

        [Test]
        public void JumpTo_PointerEqualsTwo_JumpToTwo()
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(method => method.Text()).Returns("lk34mocmt++ -=-=-= -fzfsf");

            var syntaxAnalyzer = new SyntaxAnalyzer(fileReader.Object);

            var code = new _0.Core.Code.Code(syntaxAnalyzer);
            code.JumpTo(2);
            Assert.AreEqual(code.CurrentCommand(), '-');
        }

    }
}

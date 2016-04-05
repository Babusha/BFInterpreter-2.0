using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFInterpreter_2._0.Core.Code;
using Moq;
using NUnit.Framework;

namespace BFInterpreter_2._0_Tests
{
    [TestFixture]
    public class CodeTest
    {

        [Test]
        public void Constructor_PassMessedCode_NoExceptions()
        {
            var syntaxAnalyzer = new SyntaxAnalyzer("lk34mocmt++ -=-=-= -fzfsf");
            var code = new Code(syntaxAnalyzer);
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
            var syntaxAnalyzer = new SyntaxAnalyzer("lk34mocmt+++ -=-=-= -fzfsf");
            var code = new Code(syntaxAnalyzer);
            code.JumpTo(2);
            Assert.AreEqual(code.CurrentCommand(), '+');
        }

    }
}

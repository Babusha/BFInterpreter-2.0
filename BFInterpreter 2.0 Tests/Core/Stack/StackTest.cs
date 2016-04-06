using System.Collections.Generic;
using BFInterpreter_2._0.Core.Exceptions;
using BFInterpreter_2._0.Core.Stack;
using Moq;
using NUnit.Framework;

namespace BFInterpreter_2._0_Tests.Core.Stack
{
    [TestFixture]
    public class StackTest
    {
        [Test]
        public void PeekProperty_PassEmptyStack_ExpectStackEmptyException()
        {
            var emptyStackItems = Mock.Of<IStackElems>(property => property.Items == new List<uint>());
            var stack = new _0.Core.Stack.Stack(emptyStackItems);

            Assert.Throws<StackEmptyException>(delegate
            {
                // Do something with property
               stack.Peek.GetTypeCode();
            });
        }
    }
}

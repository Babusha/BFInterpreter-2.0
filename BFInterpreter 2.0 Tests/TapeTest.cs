using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BFInterpreter_2._0.Core.Tape;
using Moq;
using NUnit.Framework;

namespace BFInterpreter_2._0_Tests
{
    [TestFixture]
    public class TapeTest
    {
        
        [Test]
        public void Next_PointerEqualsZero_ExpectPointerIncrement()
        {
            var tapeMemory = new TapeMemory();
            var tape = new Tape(tapeMemory);
            var before = tape.Pointer;
            tape.Next();
            var after = tape.Pointer;
            Assert.Greater(after, before);
        }

        [Test]
        public void Next_PointerEqualsToLastCellUintMaxValue_ExpectJumpToZero()
        {
            var tapeMemory = new TapeMemory();
            var tape = new Tape(tapeMemory);
            tape.Pointer = UInt32.MaxValue-1;;
            tape.Next();
            Assert.AreEqual(tape.Pointer, UInt16.MinValue);
        }

        [Test]
        public void Prev_PointerEqualsTwo_ExpectPointerDecrement()
        {
            var tapeMemory = new TapeMemory();
            var tape = new Tape(tapeMemory);
            var before = tape.Pointer;
            tape.Next();
            tape.Next();
            tape.Prev();
            var after = tape.Pointer;
            Assert.Greater(after, before);
        }

        [Test]
        public void Prev_PointerEqualsZero_ExpectJumpToLastCell()
        {
            var tapeMemory = new TapeMemory();
            var tape = new Tape(tapeMemory);
            tape.Prev();
            Assert.AreEqual(tape.Pointer, UInt16.MaxValue-1);
        }

        [Test]
        public void Increment_CellEqualsZero_ExpectIncrementValue()
        {
            var tapeMemory = new TapeMemory();
            var tape = new Tape(tapeMemory);
            var before = tape.Value;
            tape.Increment();
            var after = tape.Value;
            Assert.Greater(after,before);
        }

        [Test]
        public void Increment_CellEqualsInt16Max_ExpectZero()
        {
            var tapeMemory = new TapeMemory();
            var tape = new Tape(tapeMemory);
            tape.Value = Int16.MaxValue;
            tape.Increment();
            Assert.AreEqual(tape.Value, Int16.MinValue);
        }

        [Test]
        public void Increment_CellEqualsZero_ExpectInt16Min()
        {
            var tapeMemory = new TapeMemory();
            var tape = new Tape(tapeMemory);
            tape.Value = Int16.MinValue;
            tape.Decrement();
            Assert.AreEqual(tape.Value, Int16.MaxValue);
        }

    }
}

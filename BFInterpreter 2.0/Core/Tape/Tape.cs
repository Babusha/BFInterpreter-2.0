using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFInterpreter_2._0.Core.Tape
{
    public class Tape : ITape
    {
        public ITapeMemory Memory { get; set; }
        public uint Pointer { get; set; }

        public short Value
        {
            get { return Memory.Data[Pointer]; }
            set { Memory.Data[Pointer] = value; }
        }
        public Tape(ITapeMemory tapeMemory)
        {
            Memory = tapeMemory;
            Pointer = 0;
        }

        public void Next()
        {
            if (Pointer >= UInt16.MaxValue - 1)
            {
                Pointer = UInt16.MinValue;
                return;
            }
            Pointer++;
        }

        public void Prev()
        {
            if (Pointer <= UInt16.MinValue)
            {
                Pointer = UInt16.MaxValue - 1;
                return;
            }
            Pointer--;
        }

        public void Increment()
        {
            Value++;
        }

        public void Decrement()
        {
            Value--;
        }
    }
}

using System;
using System.Linq;

namespace BFInterpreter_2._0.Core.Tape
{
    public class TapeMemory : ITapeMemory
    {
        public short[] Data { get; set; }
        public TapeMemory()
        {
            Data = Enumerable.Repeat((short)0, UInt16.MaxValue).ToArray();
        }
    }
}
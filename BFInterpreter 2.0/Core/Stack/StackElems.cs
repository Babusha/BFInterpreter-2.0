using System;
using System.Collections.Generic;

namespace BFInterpreter_2._0.Core.Stack
{
    public class StackElems : IStackElems
    {
        public List<uint> Items { get; set; }

        public StackElems()
        {
            Items = new List<uint>();
        }
        public uint Count()
        {
            return (uint) Items.Count;
        }
    }
}

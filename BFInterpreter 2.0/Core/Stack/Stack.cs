using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFInterpreter_2._0.Core.Exceptions;

namespace BFInterpreter_2._0.Core.Stack
{
    public class Stack : IStack
    {
        public IStackElems Elements { get; set; }

        public uint Peek
        {
            get
            {
                if (Elements.Count() == 0)
                {
                    throw new StackEmptyException();
                }
                return Elements.Items[Elements.Items.Count - 1];
            }
        }

        public Stack(IStackElems elements)
        {
            Elements = elements;
        }

        public void Push(uint item)
        {
            Elements.Items.Add(item);
        }

        public uint Pop()
        {
            var item = Elements.Items[Elements.Items.Count - 1];
            Elements.Items.RemoveAt(Elements.Items.Count - 1);
            return item;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BFInterpreter_2._0.Core.Stack
{
    public interface IStack
    {
        IStackElems Elements { get; set; }
        uint Peek { get; }
        void Push(uint item);
        uint Pop();
    }
}

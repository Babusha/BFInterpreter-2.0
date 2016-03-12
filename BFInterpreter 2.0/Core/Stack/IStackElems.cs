using System.Collections.Generic;

namespace BFInterpreter_2._0.Core.Stack
{
    public interface IStackElems
    {
        List<uint> Items { get; set; }
        uint Count();
    }
}

using System.Collections.Generic;

namespace BFInterpreter_2._0.Core.Code
{
    public interface ICode
    {
        void JumpTo(uint pointer);
        char CurrentCommand();
        List<char> Commands { get; set; }
        uint Pointer { get; set; }
        bool Ends();
        void Next();

    }
}
using BFInterpreter_2._0.Core.Code;
using BFInterpreter_2._0.Core.Runtime.InputOutput;
using BFInterpreter_2._0.Core.Tape;

namespace BFInterpreter_2._0.Core.Runtime.Interpreter
{
    public interface IMachine
    {
        ITape Tape { get; set; }
        IInputOutput InputOutput { get; set; }
        ICode Code { get; set; }
        void Increment();
        void Decrement();
        void Next();
        void Prev();
        void BeginLoop();
        void EndLoop();
        void Input();
        void Output();
    }
}
using BFInterpreter_2._0.Core.Code;
using BFInterpreter_2._0.Core.Tape;

namespace BFInterpreter_2._0.Core.Interpreter
{
    public interface IMachine
    {
        ITape Tape { get; set; }
        ICode Code { get; set; }
        IInputOutput InputOutput { get; set; }
        void Inc();
        void Dec();
        void Next();
        void Prev();
    }
}
using BFInterpreter_2._0.Core.Runtime.Interpreter;
using BFInterpreter_2._0.Core.Runtime.Machine;

namespace BFInterpreter_2._0.Core.Runtime
{
    public interface IRuntime
    {
        IMachine Machine { get; set; }
        void Run();
    }
}
using BFInterpreter_2._0.Core.Code;

namespace BFInterpreter_2._0.Core.Runtime.Interpreter
{
    public interface IInterpreter
    {
        IMachine Machine { get; set; }
        int Run();
    }
}
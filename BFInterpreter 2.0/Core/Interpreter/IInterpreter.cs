namespace BFInterpreter_2._0.Core.Interpreter
{
    public interface IInterpreter
    {
        IMachine Machine { get; set; }
        int Run();
    }
}
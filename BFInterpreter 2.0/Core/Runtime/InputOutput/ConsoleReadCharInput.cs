using System;

namespace BFInterpreter_2._0.Core.Runtime.InputOutput
{
    // Many programs have problems with this type of input
    public class ConsoleReadCharInput : IInputOutput
    {
        public void Output(char character)
        {
            Console.Write(character);
        }
        public char Input()
        {
            return (char) Console.Read();
        }
    }
}

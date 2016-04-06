using System;

namespace BFInterpreter_2._0.Core.Runtime.InputOutput
{

    public class ConsoleReadKeyCharInput : IInputOutput
    {
        public void Output(char character)
        {
            Console.Write(character);
        }
        public char Input()
        {
            return Console.ReadKey().KeyChar;
        }
    }
}

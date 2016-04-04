using System;

namespace BFInterpreter_2._0.Core.Runtime.InputOutput
{
    public class InputOutput
    {
        public void Output(char character)
        {
            Console.WriteLine(character);
        }
        public char Input()
        {
            return (char) Console.Read();
        }
    }
}

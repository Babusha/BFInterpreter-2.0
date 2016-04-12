using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BFInterpreter_2._0.Core.Runtime.InputOutput
{
    public class GetcharPutcharInputOutput : IInputOutput
    {
        [DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
        private static extern int putchar(int character);
        [DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
        private static extern int getchar();
        public void Output(char character)
        {
            putchar(character);
        }
        
        public char Input()
        {
            return (char) getchar();
        }
    }
}

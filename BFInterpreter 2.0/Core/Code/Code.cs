﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFInterpreter_2._0.Core.Code
{
    public class Code : ICode
    {
        public uint Pointer { get; set; }

        public List<char> Commands { get; set; }
        public Code(ISyntaxAnalyzer syntaxAnalyzer)
        {
            Commands = syntaxAnalyzer.Code.ToList();
        }

        public void Next()
        {
            if (Ends())
            {
                throw new InvalidProgramException("Pointer is greater than program code");
            }
            Pointer++;
        }

        public void Prev()
        {
            if (Ends())
            {
                throw new InvalidProgramException("Pointer is greater than program code");
            }
            Pointer++;
        }

        public void JumpTo(uint pointer)
        {
            if (Ends())
            {
                throw new InvalidProgramException("Pointer is greater than program code");
            }
            Pointer = pointer;
        }

        public char CurrentCommand()
        {
            return Commands[(int) Pointer];
        }

        public bool Ends()
        {
            return Pointer >= Commands.Count;
        }

    }
}
 
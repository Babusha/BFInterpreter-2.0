﻿using System;
using BFInterpreter_2._0.Core.Code;
using BFInterpreter_2._0.Core.Runtime.InputOutput;
using BFInterpreter_2._0.Core.Stack;
using BFInterpreter_2._0.Core.Tape;

namespace BFInterpreter_2._0.Core.Runtime.Interpreter
{
    public class Machine : IMachine
    {
        public IInputOutput InputOutput { get; set; }
        public ITape Tape { get; set; }
        public IStack Stack { get; set; }
        public ICode Code { get; set; }
        private int _loopDepth;
        public Machine(IInputOutput inputOutput, ITape tape, IStack stack, ICode code)
        {
            InputOutput = inputOutput;
            Tape = tape;
            Stack = stack;
            Code = code;
            _loopDepth = 0;
        }
        public void Increment()
        {
            Tape.Increment();
        }

        public void Decrement()
        {
            Tape.Decrement();
        }

        public void Next()
        {
            Tape.Next();
        }

        public void Prev()
        {
            Tape.Prev();
        }

        public void BeginLoop()
        {
            if (Tape.Value.Equals(0))
            {
                Stack.Push(Code.Pointer);
            }
            else
            {
                _loopDepth++;
                while (_loopDepth > 0)
                {
                    Code.Next();
                    if (Code.CurrentCommand().Equals(']'))
                    {
                        _loopDepth--;
                    }
                    if (Code.CurrentCommand().Equals('['))
                    {
                        _loopDepth++;
                    }
                }
            }
        }

        public void EndLoop()
        {
            if (!Tape.Value.Equals(0))
            {
                Code.JumpTo(Stack.Peek);
            }
            else
            {
                Stack.Pop();
            }
        }

        public void Output()
        {
            InputOutput.Output((char) Tape.Value);
        }

        public void Input()
        {
            Tape.Value = (short) InputOutput.Input();
        }
    }
}

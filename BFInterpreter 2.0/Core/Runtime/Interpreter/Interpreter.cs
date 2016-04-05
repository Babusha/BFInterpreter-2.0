using System;
using System.Collections;
using BFInterpreter_2._0.Core.Code;
using System.Collections.Generic;
using BFInterpreter_2._0.Core.Runtime.Machine;

namespace BFInterpreter_2._0.Core.Runtime.Interpreter
{
    public class Interpreter : IRuntime
    {
        public IMachine Machine { get; set; }
        private readonly IDictionary<char,Action> _executeCommand;

        public Interpreter(IMachine machine)
        {
            Machine = machine;
            _executeCommand = new Dictionary<char, Action>
            {
                {'+', () => Machine.Increment()},
                {'-', () => Machine.Decrement()},
                {'>', () => Machine.Next()},
                {'<', () => Machine.Prev()},
                {'.', () => Machine.Output()},
                {',', () => Machine.Input()},
                {'[', () => Machine.BeginLoop()},
                {']', () => Machine.EndLoop()}
            };

        }
        public void Run()
        {
            while (!Machine.Code.Ends())
            {
                _executeCommand[Machine.Code.CurrentCommand()]();
                Machine.Code.Next();
            }
        }
    }
}
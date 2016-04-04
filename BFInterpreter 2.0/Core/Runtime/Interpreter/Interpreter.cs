using System;
using System.Collections;
using BFInterpreter_2._0.Core.Code;
using System.Collections.Generic;

namespace BFInterpreter_2._0.Core.Runtime.Interpreter
{
    public class Interpreter
    {
        public IMachine Machine { get; set; }
        private readonly IDictionary<char,Action> _executeCommand;

        public Interpreter(IMachine machine)
        {
            Machine = machine;
            _executeCommand = new Dictionary<char, Action>();
            _executeCommand.Add('+', () => Machine.Increment());
            _executeCommand.Add('-', () => Machine.Decrement());
            _executeCommand.Add('>', () => Machine.Next());
            _executeCommand.Add('<', () => Machine.Prev());
            _executeCommand.Add('.', () => Machine.Output());
            _executeCommand.Add(',', () => Machine.Input());
            _executeCommand.Add('[', () => Machine.BeginLoop());
            _executeCommand.Add(']', () => Machine.EndLoop());

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
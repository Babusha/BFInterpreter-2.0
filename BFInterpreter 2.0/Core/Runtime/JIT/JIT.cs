using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFInterpreter_2._0.Core.Runtime.Machine;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace BFInterpreter_2._0.Core.Runtime.JIT
{
    public class Scope
    {
        public IMachine Machine { get; set; }
    }
    public class JIT : IRuntime
    {
        public IMachine Machine { get; set; }
        private string _codeEvaluate;
        public JIT(IMachine machine)
        {
            Machine = machine;
            _codeEvaluate = String.Empty;
        }
        public void Run()
        {
            CSharpCodeGenerate();
            throw new NotImplementedException();

        }

        private void CSharpCodeGenerate()
        {
            var compile = new Dictionary<char, string>
            {
                {'+', "Machine.Increment();"},
                {'-', "Machine.Decrement();"},
                {'>', "Machine.Next();"},
                {'<', "Machine.Prev();"},
                {'.', "Machine.Input();"},
                {',', "Machine.Output();"},
                {'[', "while(Machine.Tape.Value.Equals(0))\n{"},
                {']', "}"}
            };

            _codeEvaluate = Machine.Code
                .Commands
                .Aggregate("",
                    (current, item) => current + compile[item] + "\n");
            
        }
    }
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BFInterpreter_2._0.Core.Runtime.Machine;
using Microsoft.CSharp;

namespace BFInterpreter_2._0.Core.Runtime.JIT
{
    public class JITUnsafe : IRuntime
    {
        public IMachine Machine { get; set; }
        public JITUnsafe(IMachine machine)
        {
            Machine = machine;
        }
        public void Run()
        {
            var compile = new Dictionary<char, string>
            {
                {'>', "++stackPointer; "},
                {'<', "--stackPointer; "},
                {'+', "++(*stackPointer); "},
                {'-', "--(*stackPointer); "},
                {',', @"*stackPointer = (Byte) inputOutput.Input(); if(*stackPointer == 13) { *stackPointer = 10; inputOutput.Output('\n'); }"},
                {'.', "inputOutput.Output((char) *stackPointer); "},
                {'[', "while (*stackPointer != 0) { "},
                {']', " }"}
            };

            string codeEvaluate = Machine.Code
                .Commands
                .Aggregate(String.Empty,
                    (current, item) => current + compile[item] + "\n");

            var compilerParameters = new CompilerParameters
            {
                CompilerOptions = "/unsafe"
            };

            compilerParameters
                .ReferencedAssemblies
                .Add("system.dll");

            compilerParameters
                .ReferencedAssemblies
                .Add(Assembly.GetExecutingAssembly().Location);

            compilerParameters
                .GenerateExecutable = false;

            compilerParameters
                .GenerateInMemory = true;

            var code = new StringBuilder();

            code.Append("using System; \n");
            code.Append("namespace Brain { \n");
            code.Append("  public class Fuck { \n");
            code.Append("       static unsafe Byte * stackPointer;");
            code.Append("       public static unsafe void Execute(BFInterpreter_2._0.Core.Runtime.InputOutput.IInputOutput inputOutput) {\n");
            code.Append($"          Byte * stack = stackalloc Byte[{UInt16.MaxValue}];");
            code.Append("           stackPointer = stack;");
            code.Append("           " + codeEvaluate);
            code.Append("       }\n");
            code.Append("   }\n");
            code.Append("}\n"); 

            var provider = new CSharpCodeProvider();
            CompilerResults result = provider
                .CompileAssemblyFromSource(compilerParameters, code.ToString());

            var method = result
                .CompiledAssembly
                .GetType("Brain.Fuck")
                .GetMethod("Execute", BindingFlags.Static | BindingFlags.Public);

            method.Invoke(result, new object[] { Machine.InputOutput});
        }
    }
}

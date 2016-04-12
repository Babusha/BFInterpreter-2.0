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
            var compile = new StringBuilder();
            foreach (var item in Machine.Code.Commands)
            {
                switch (item)
                {
                    case '+':
                        compile.Append("++(*tapePointer);");
                        break;
                    case '-':
                        compile.Append("--(*tapePointer);  ");
                        break;
                    case '>':
                        compile.Append("++tapePointer; ");
                        break;
                    case '<':
                        compile.Append("--tapePointer; ");
                        break;
                    case '.':
                        compile.Append("inputOutput.Output((char) *tapePointer); ");
                        break;
                    case ',':
                        compile.Append(
                            @"*tapePointer = (Byte) inputOutput.Input(); if(*tapePointer == 13) { *tapePointer = 10; inputOutput.Output('\n'); }");
                        break;
                    case '[':
                        compile.Append("while (*tapePointer != 0) { ");
                        break;
                    case ']':
                        compile.Append(" }");
                        break;

                }
                
            }
            var codeEvaluate = compile.ToString();

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
            code.Append("       static unsafe Byte * tapePointer;");
            code.Append("       public static unsafe void Execute(BFInterpreter_2._0.Core.Runtime.InputOutput.IInputOutput inputOutput) {\n");
            code.Append($"          Byte * tape = stackalloc Byte[30000];");
            code.Append("           tapePointer = tape;");
            code.Append(codeEvaluate);
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

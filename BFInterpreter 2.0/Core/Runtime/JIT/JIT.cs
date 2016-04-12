using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BFInterpreter_2._0.Core.Runtime.Machine;
using Microsoft.CSharp;

namespace BFInterpreter_2._0.Core.Runtime.JIT
{
    public class JIT : IRuntime
    {
        public IMachine Machine { get; set; }
        public JIT(IMachine machine)
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
                        compile.Append("machine.Increment();");
                        break;
                    case '-':
                        compile.Append("machine.Decrement();   ");
                        break;
                    case '>':
                        compile.Append("machine.Next(); ");
                        break;
                    case '<':
                        compile.Append("machine.Prev(); ");
                        break;
                    case '.':
                        compile.Append("machine.Output();");
                        break;
                    case ',':
                        compile.Append("machine.Input();");
                        break;
                    case '[':
                        compile.Append("while(!machine.Tape.Value.Equals(0)) {");
                        break;
                    case ']':
                        compile.Append(" }");
                        break;

                }

            }
            var codeEvaluate = compile.ToString();

            var compilerParameters = new CompilerParameters();

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
            code.Append("       public static void Execute(BFInterpreter_2._0.Core.Runtime.Machine.Machine machine) {\n");
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
            method.Invoke(result, new object[] { Machine });
        }
    }
}

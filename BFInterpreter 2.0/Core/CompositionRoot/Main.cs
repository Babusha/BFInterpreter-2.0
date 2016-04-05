using System;
using Autofac;
using BFInterpreter_2._0.Core.Code;
using BFInterpreter_2._0.Core.File;
using BFInterpreter_2._0.Core.Runtime;
using BFInterpreter_2._0.Core.Runtime.InputOutput;
using BFInterpreter_2._0.Core.Runtime.Interpreter;
using BFInterpreter_2._0.Core.Runtime.JIT;
using BFInterpreter_2._0.Core.Runtime.Machine;
using BFInterpreter_2._0.Core.Stack;
using BFInterpreter_2._0.Core.Tape;

namespace BFInterpreter_2._0.Core.CompositionRoot
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[] { "hello.bf" };
            Bootstrapper.InitializeBuilder();

            Bootstrapper.Builder
                .Register(c => new File.File(args[0]))
                .As<IFile>();

            Bootstrapper.Builder
                .RegisterType<TapeMemory>()
                .As<ITapeMemory>();

            Bootstrapper.Builder
                .RegisterType<InputOutput>()
                .As<IInputOutput>();

            Bootstrapper.Builder
                .RegisterType<StackElems>()
                .As<IStackElems>();

            Bootstrapper.SetAutofacContainer();


            var file = Bootstrapper
                .GetService<IFile>();
            var tapeMemory = Bootstrapper
                .GetService<ITapeMemory>();
            var inputOutput = Bootstrapper
                .GetService<IInputOutput>();
            var stackElems = Bootstrapper
                .GetService<IStackElems>();

            Bootstrapper.InitializeBuilder();

            Bootstrapper.Builder
                .Register(c => new FileProvider(file))
                .As<IFileProvider>();

            Bootstrapper.Builder
                .Register(c => new Tape.Tape(tapeMemory))
                .As<ITape>();

            Bootstrapper.Builder
               .Register(c => new Stack.Stack(stackElems))
               .As<IStack>();
            Bootstrapper.SetAutofacContainer();


            var fileProvider = Bootstrapper.GetService
                <IFileProvider>();
            var stack = Bootstrapper.GetService
                <IStack>();
            var tape = Bootstrapper.GetService
                <ITape>();


            Bootstrapper.InitializeBuilder();

            Bootstrapper.Builder
                .Register(c => new FileReader(fileProvider))
                .As<IFileReader>();

            Bootstrapper.SetAutofacContainer();


            var fileReader = Bootstrapper.GetService<IFileReader>();

            Bootstrapper.InitializeBuilder();
            Bootstrapper.Builder
                .Register(c => new SyntaxAnalyzer(fileReader.Text()))
                .As<ISyntaxAnalyzer>();
            Bootstrapper.SetAutofacContainer();
            
            var syntaxAnalyzer = Bootstrapper.GetService<ISyntaxAnalyzer>();

            Bootstrapper.InitializeBuilder();
            Bootstrapper.Builder
                .Register(c => new Code.Code(syntaxAnalyzer))
                .As<ICode>();
            Bootstrapper.SetAutofacContainer();

            var code = Bootstrapper.GetService<ICode>();

            Bootstrapper.InitializeBuilder();
            Bootstrapper.Builder
                .Register(c => new Machine(inputOutput,tape,stack,code))
                .As<IMachine>();
            Bootstrapper.SetAutofacContainer();

            var machine = Bootstrapper.GetService<IMachine>();
            Bootstrapper.InitializeBuilder();

            Bootstrapper.Builder
                .Register(c => new Interpreter(machine))
                //.Register(c => new JIT(machine))
                .As<IRuntime>();
            Bootstrapper.SetAutofacContainer();

            var interpreter = Bootstrapper.GetService
                <IRuntime>();

            interpreter.Run();
        }
    }
}

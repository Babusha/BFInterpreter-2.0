using Autofac;
using BFInterpreter_2._0.Core.Code;
using BFInterpreter_2._0.Core.File;
using BFInterpreter_2._0.Core.Runtime.InputOutput;
using BFInterpreter_2._0.Core.Runtime.Interpreter;
using BFInterpreter_2._0.Core.Stack;
using BFInterpreter_2._0.Core.Tape;

namespace BFInterpreter_2._0.Core.CompositionRoot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper.InitializeBuilder();

            // FileReader
            Bootstrapper.Builder
                .Register(c => new File.File(args[0]))
                .As<IFile>();

            Bootstrapper.Builder
                .Register(c => new FileProvider(Bootstrapper
                    .Container.Resolve<IFile>()))
                .As<IFileProvider>();

            Bootstrapper.Builder
                .Register(c => new FileReader(Bootstrapper.Container
                    .Resolve<IFileProvider>()))
                .As<IFileReader>();


            // Code
            Bootstrapper.Builder
                .Register(c => new SyntaxAnalyzer(Bootstrapper.Container
                    .Resolve<IFileReader>()
                    .Text()))
                .As<ISyntaxAnalyzer>();

            Bootstrapper.Builder
                .Register(c => new Code.Code(Bootstrapper.Container
                    .Resolve<ISyntaxAnalyzer>()))
                .As<ICode>();

            // Stack
            Bootstrapper.Builder
                .RegisterType<StackElems>()
                .As<IStackElems>();

            Bootstrapper.Builder
                .Register(c => new Stack.Stack(Bootstrapper.Container
                    .Resolve<IStackElems>()))
                .As<IStack>();

            // Tape
            Bootstrapper.Builder
                .RegisterType<TapeMemory>()
                .As<ITapeMemory>();

            Bootstrapper.Builder
                .Register(c => new Tape.Tape(Bootstrapper.Container
                    .Resolve<ITapeMemory>()))
                .As<ITape>();

            // InputOutput
            Bootstrapper.Builder
                .RegisterType<InputOutput>()
                .As<IInputOutput>();

            // Machine
            var inputOutput = Bootstrapper.Container
                .Resolve<IInputOutput>();

            var tape = Bootstrapper.Container
                .Resolve<ITape>();

            var stack = Bootstrapper.Container
                .Resolve<IStack>();

            var code = Bootstrapper.Container
                .Resolve<ICode>();

            Bootstrapper.Builder
                .Register(c => new Machine(inputOutput,
                    tape,
                    stack,
                    code))
                .As<IMachine>();

            Bootstrapper.SetAutofacContainer();

        }
    }
}

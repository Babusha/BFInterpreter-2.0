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
            args = new string[] { "hello.bf" };
            Bootstrapper.InitializeBuilder();

            // FileReader
            Bootstrapper.Builder
                .Register(c => new File.File(args[0]))
                .As<IFile>();

            Bootstrapper.Builder
                .Register(c => new FileProvider(Bootstrapper
                    .GetService<IFile>()))
                .As<IFileProvider>();

            Bootstrapper.InitializeBuilder();
            Bootstrapper.Builder
                .Register(c => new FileReader(Bootstrapper.GetService
                    <IFileProvider>()))
                .As<IFileReader>();

            // Code
            Bootstrapper.Builder
                .Register(c => new SyntaxAnalyzer(Bootstrapper.GetService
                    <IFileReader>()
                    .Text()))
                .As<ISyntaxAnalyzer>();

            Bootstrapper.Builder
                .Register(c => new Code.Code(Bootstrapper.GetService
                    <ISyntaxAnalyzer>()))
                .As<ICode>();

            // Stack
            Bootstrapper.Builder
                .RegisterType<StackElems>()
                .As<IStackElems>();

            Bootstrapper.Builder
                .Register(c => new Stack.Stack(Bootstrapper.GetService
                    <IStackElems>()))
                .As<IStack>();

            // Tape
            Bootstrapper.Builder
                .RegisterType<TapeMemory>()
                .As<ITapeMemory>();

            Bootstrapper.Builder
                .Register(c => new Tape.Tape(Bootstrapper.GetService
                    <ITapeMemory>()))
                .As<ITape>();

            // InputOutput
            Bootstrapper.Builder
                .RegisterType<InputOutput>()
                .As<IInputOutput>();

            Bootstrapper.Builder
                .Register(c => new Machine(Bootstrapper.GetService
                    <IInputOutput>(),
                    Bootstrapper.GetService
                        <ITape>(),
                    Bootstrapper.GetService
                        <IStack>(),
                    Bootstrapper.GetService
                        <ICode>()))
                .As<IMachine>();

            // Interpreter
            Bootstrapper.Builder
                .Register(c => new Interpreter(Bootstrapper.GetService
                    <IMachine>()))
                .As<IInterpreter>();
            Bootstrapper.SetAutofacContainer();

            var interpreter = Bootstrapper.GetService
                <IInterpreter>();

            interpreter.Run();
        }
    }
}

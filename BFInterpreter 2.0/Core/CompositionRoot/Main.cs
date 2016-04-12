using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
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
using NDesk.Options;

namespace BFInterpreter_2._0.Core.CompositionRoot
{
    class Program
    {
        private enum Runtime
        {
            Interpreter,
            JitSafe,
            JitUnsafe
        }

        private enum InputOutput
        {
            Console,
            File
        }

        static void Main(string[] args)
        {
           args = new[] {"tower.bf", "-ju", "-t"};
            Stopwatch watch = null;
            Version version = Assembly.GetExecutingAssembly()
                .GetName()
                .Version;

            bool showTimer = false;
            bool showHelp = false;
            var runtime = Runtime.JitUnsafe;
            var setInputOutput = InputOutput.Console;

            string inputFilePath = null;
            string outputFilePath = null;

            var help = $"Use bf.exe <path>:\n" +
                       "-h|?|help      Show help\n" +
                       "-t|timer       Timer\n" +
                       "-i             Use interpreter as runtime\n" +
                       "-js            Use safe C# JIT code generatiion as runtime\n" +
                       "-ju            Use unsafe C# JIT code generation as runtime\n" +
                       "-if            Set input file path\n" +
                       "-of            Set output file path\n" +
                       "-f             Use deafault input.txt and output.txt files for IO\n" +
                       "Version: " + version;    

            var p = new OptionSet
            {
                {"h|?|help", h => showHelp = true},
                {"t|timer", t => showTimer = true},
                {"i", i => runtime = Runtime.Interpreter},
                {"js", js => runtime = Runtime.JitSafe},
                {"ju", ju => runtime = Runtime.JitUnsafe},
                {"if", delegate(string v)
                {
                    setInputOutput = InputOutput.File;
                    inputFilePath = v;
                }},
                {"of", delegate(string v)
                {
                    setInputOutput = InputOutput.File;
                    outputFilePath = v;
                }},
                {"f", f => setInputOutput = InputOutput.File }
            };

            List<string> extra = p.Parse(args);

            if (showHelp || args.Length == 0)
            {
                Console.WriteLine(help);
                return;
            }
            if (showTimer)
            {
                watch = Stopwatch.StartNew();
            }

            var builder = new ContainerBuilder();

            builder
                .Register(c => new File.File(extra[0]))
                .As<IFile>();

            builder
                .RegisterType<TapeMemory>()
                .As<ITapeMemory>();

            builder
                .RegisterType<StackElems>()
                .As<IStackElems>();

            builder
                .RegisterType<FileProvider>()
                .As<IFileProvider>();

            builder
                .RegisterType<Tape.Tape>()
                .As<ITape>();

            builder
                .RegisterType<Stack.Stack>()
                .As<IStack>();

            builder
                .RegisterType<FileReader>()
                .As<IFileReader>();

            builder
                .RegisterType<SyntaxAnalyzer>()
                .As<ISyntaxAnalyzer>();

            builder
                .RegisterType<Code.Code>()
                .As<ICode>();

            builder
                .RegisterType<Machine>()
                .As<IMachine>();

            switch (runtime)
            {
                case Runtime.Interpreter:
                    builder
                        .RegisterType<Interpreter>()
                        .As<IRuntime>();
                    break;
                case Runtime.JitSafe:
                    builder
                        .RegisterType<JIT>()
                        .As<IRuntime>();
                    break;
                case Runtime.JitUnsafe:
                    builder
                        .RegisterType<JITUnsafe>()
                        .As<IRuntime>();
                    break;
            }

            switch (setInputOutput)
            {
                case InputOutput.Console:
                    builder
                        //.RegisterType<ConsoleReadKeyCharInput>()
                        .RegisterType<GetcharPutcharInputOutput>()
                        .As<IInputOutput>();
                    break;
                case InputOutput.File:
                    if (inputFilePath == null)
                    {
                        inputFilePath = "input.txt";
                    }
                    if (outputFilePath == null)
                    {
                        outputFilePath = "output.txt";
                    }
                    builder
                        .Register(c => new FileInputOutput(inputFilePath, outputFilePath))
                        .As<IInputOutput>();
                    break;
            }

            IContainer container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var interpreter = scope.Resolve<IRuntime>();
                interpreter.Run();
                if(showTimer)
                {
                    using (var inputOutputScope = container.BeginLifetimeScope())
                    {
                        watch.Stop();
                        var elapsedMs = watch.ElapsedMilliseconds;
                        foreach (var character in $"\nTime: {elapsedMs / 1000.0} seconds\n".ToCharArray())
                        {
                            inputOutputScope.Resolve<IInputOutput>().Output(character);
                        }
                    }
                }
            }
        }
    }
}

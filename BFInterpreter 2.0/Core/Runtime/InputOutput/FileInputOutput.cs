using System.IO;

namespace BFInterpreter_2._0.Core.Runtime.InputOutput
{
    public class FileInputOutput : IInputOutput
    {
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        private char[] InputCharacters { get; }
        private int InputCharacterPointer { get; set; } = 0;

        public FileInputOutput(string inputPath, string outputPath)
        {
            InputPath = inputPath;
            OutputPath = outputPath;

            System.IO.File.WriteAllText(OutputPath, string.Empty);
            InputCharacters = System.IO.File.ReadAllText(InputPath).ToCharArray();
        }

        public void Output(char character)
        {
            using (var fileStream = new FileStream(OutputPath,
                        FileMode.Append,
                        FileAccess.Write))
            using (var sw = new StreamWriter(fileStream))
            {
                sw.Write(character);
            }
        }

        public char Input()
        {
            return InputCharacters[InputCharacterPointer++];
        }
    }
}

using System;
using System.IO;
using BFInterpreter_2._0.Core.Exceptions;

namespace BFInterpreter_2._0.Core.File
{
    public class FileReader : IFileReader
    {
        public IFileProvider FileProvider { get; set; }

        public FileReader(IFileProvider fileProvider)
        {
            FileProvider = fileProvider;
            if (!FileProvider.Exists())
            {
                throw new FileNotFoundException();
            }
            if (!FileProvider.IsValidExtension())
            {
                throw new InvalidFileExtensionException();
            }
        }
        public string Text()
        {
            string text = System
                .IO
                .File
                .ReadAllText(FileProvider.File.Path);
            return text;
        }
    }
}

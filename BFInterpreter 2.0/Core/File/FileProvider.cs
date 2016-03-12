using System;
using System.IO;
using BFInterpreter_2._0.Core.Exceptions;

namespace BFInterpreter_2._0.Core.File
{
    public class FileProvider : IFileProvider
    {
        public IFile File { get; set; }

        public FileProvider(IFile fileP)
        {
            File = File;
        }
        public bool Exists()
        {
            return System.IO.File.Exists(File.Path);
        }

        public bool IsValidExtension()
        {
            return System.IO.Path.GetExtension(File.Path).Equals(".bf");
        }

    }
}

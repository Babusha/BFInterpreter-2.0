namespace BFInterpreter_2._0.Core.File
{
    public class File : IFile
    {
        public string Path { get; set; }

        public File(string path)
        {
            Path = path;
        }
    }
}
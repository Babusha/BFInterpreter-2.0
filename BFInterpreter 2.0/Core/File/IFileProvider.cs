namespace BFInterpreter_2._0.Core.File
{
    public interface IFileProvider
    {
        IFile File { get; set; }
        bool Exists();
        bool IsValidExtension();
    }
}
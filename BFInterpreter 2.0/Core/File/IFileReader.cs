namespace BFInterpreter_2._0.Core.File
{
    public interface IFileReader
    {
        IFileProvider FileProvider { get; set; }
        string Text();
    }
}
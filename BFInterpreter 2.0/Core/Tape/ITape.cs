namespace BFInterpreter_2._0.Core.Tape
{
    public interface ITape
    {
        ITapeMemory Memory { get; set; }
        uint Pointer { get; set; }
        short Value { get; set; }
        void Next();
        void Prev();
        void Increment();
        void Decrement();

    }
}
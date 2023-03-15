namespace EntiCS.Profiling
{
    public interface IProfilerSettings
    {
        bool IsEnabled { get; }
        int DefaultBufferLength { get; }
    }
}
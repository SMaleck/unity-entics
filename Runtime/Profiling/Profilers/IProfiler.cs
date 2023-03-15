namespace EntiCS.Profiling.Profilers
{
    public interface IProfiler
    {
        void LogResults();
        string GetLogableResults();
    }
}
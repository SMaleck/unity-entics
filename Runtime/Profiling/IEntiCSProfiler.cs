using System;

namespace EntiCS.Profiling
{
    public interface IEntiCSProfiler : IDisposable
    {
        bool IsEnabled { get; }

        void LogResults();
    }
}

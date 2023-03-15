using EntiCS.Profiling.Profilers;
using EntiCS.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntiCS.Profiling
{
    public class EntiCSProfiler : IEntiCSProfiler
    {
        private readonly IProfilerSettings _settings;
        private readonly IProfiler[] _profilers;
        private readonly StringBuilder _sb = new StringBuilder();

        public bool IsEnabled => _settings.IsEnabled;

        public EntiCSProfiler(
            IProfilerSettings settings,
            IEnumerable<IProfiler> profilers)
        {
            _settings = settings;
            _profilers = profilers.ToArray();
        }

        public void LogResults()
        {
            if (!IsEnabled) return;

            _sb.Clear();
            _sb.AppendLine($"[{nameof(EntiCSProfiler)}] Results of all profilers:");

            foreach (var profiler in _profilers)
            {
                _sb.AppendLine(profiler.GetLogableResults());
            }

            EntiCSLog.Log(_sb.ToString());
        }

        public void Dispose()
        {
            LogResults();
        }
    }
}

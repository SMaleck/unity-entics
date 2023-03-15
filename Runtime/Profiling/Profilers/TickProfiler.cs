using EntiCS.Profiling.Utility;
using EntiCS.Systems;
using EntiCS.Ticking;
using EntiCS.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntiCS.Profiling.Profilers
{
    public class TickProfiler : IProfiler, IDisposable
    {
        private readonly IProfilerSettings _settings;
        private readonly Dictionary<string, RollingStopwatch> _stopWatches;
        private readonly StringBuilder _sb = new StringBuilder();

        private string UpdateKey => "Ticker_Update";
        private string FixedKey => "Ticker_Fixed";
        private string LateKey => "Ticker_Late";

        public TickProfiler(IProfilerSettings settings, ITicker worldTicker)
        {
            _settings = settings;
            if (!_settings.IsEnabled) return;

            _stopWatches = new Dictionary<string, RollingStopwatch>()
            {
                { UpdateKey, new RollingStopwatch(UpdateKey, settings.DefaultBufferLength) },
                { FixedKey, new RollingStopwatch(FixedKey, settings.DefaultBufferLength) },
                { LateKey, new RollingStopwatch(LateKey, settings.DefaultBufferLength) }
            };

            worldTicker.OnBeforeTick += BeforeTick;
            worldTicker.OnAfterTick += AfterTick;
        }

        public void LogResults()
        {
            if (!_settings.IsEnabled) return;
            EntiCSLog.Log(GetLogableResults());
        }

        public string GetLogableResults()
        {
            _sb.Clear();
            foreach (var stopWatch in _stopWatches.Values)
            {
                _sb.AppendLine($"[{nameof(TickProfiler)}] {stopWatch.Key}:\t {stopWatch.GetAverageMs():F3}ms");
            }

            return _sb.ToString();
        }

        public void Dispose()
        {
            foreach (var kvp in _stopWatches)
            {
                kvp.Value.Stop();
            }
        }

        private void BeforeTick(TickType updateType)
        {
            if (!_settings.IsEnabled) return;
            switch (updateType)
            {
                case TickType.Update:
                    StartWatch(UpdateKey);
                    break;

                case TickType.FixedUpdate:
                    StartWatch(FixedKey);
                    break;

                case TickType.LateUpdate:
                    StartWatch(LateKey);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(updateType), updateType, null);
            }
        }

        private void AfterTick(TickType updateType)
        {
            if (!_settings.IsEnabled) return;
            switch (updateType)
            {
                case TickType.Update:
                    StopWatch(UpdateKey);
                    break;

                case TickType.FixedUpdate:
                    StopWatch(FixedKey);
                    break;

                case TickType.LateUpdate:
                    StopWatch(LateKey);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(updateType), updateType, null);
            }
        }

        private void StartWatch(string key)
        {
            _stopWatches[key].Start();
        }

        private void StopWatch(string key)
        {
            _stopWatches[key].Stop();
        }
    }
}

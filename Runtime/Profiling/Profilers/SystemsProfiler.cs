using EntiCS.Entities;
using EntiCS.Entities.Components;
using EntiCS.Profiling.Utility;
using EntiCS.Systems;
using EntiCS.Ticking;
using EntiCS.Utility;
using EntiCS.WorldManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntiCS.Profiling.Profilers
{
    public class SystemsProfiler : IProfiler, IDisposable
    {
        private readonly IProfilerSettings _settings;

        private class ProfilerSystem : IEntitySystem
        {
            private readonly Action<string> _onUpdate;
            private readonly string _key;

            public TickType UpdateType { get; }
            public int ExecutionOrder { get; }

            public Type[] Filter { get; } = new[]
            {
                typeof(IEntityComponent)
            };

            public ProfilerSystem(TickType updateType, int executionOrder, Action<string> onUpdate, string key)
            {
                _onUpdate = onUpdate;
                _key = key;
                UpdateType = updateType;
                ExecutionOrder = executionOrder;
            }

            public void Update(float elapsedSeconds, IReadOnlyCollection<IEntity> entities)
            {
                _onUpdate(_key);
            }
        }

        private readonly Dictionary<string, RollingStopwatch> _stopWatches;
        private readonly StringBuilder _sb = new StringBuilder();

        private const string UpdateKey = "Systems_Update";
        private const string FixedKey = "Systems_Fixed";
        private const string LateKey = "Systems_Late";

        public SystemsProfiler(IProfilerSettings settings, IEntiCSRunner runner)
        {
            _settings = settings;
            if (!_settings.IsEnabled) return;

            _stopWatches = new Dictionary<string, RollingStopwatch>()
            {
                { UpdateKey, new RollingStopwatch(UpdateKey, settings.DefaultBufferLength) },
                { FixedKey, new RollingStopwatch(FixedKey, settings.DefaultBufferLength) },
                { LateKey, new RollingStopwatch(LateKey, settings.DefaultBufferLength) }
            };

            var systems = new IEntitySystem[]
            {
                new ProfilerSystem(TickType.Update, Int32.MinValue, StartWatch, UpdateKey),
                new ProfilerSystem(TickType.Update, Int32.MaxValue, StopWatch, UpdateKey),

                new ProfilerSystem(TickType.FixedUpdate, Int32.MinValue, StartWatch, FixedKey),
                new ProfilerSystem(TickType.FixedUpdate, Int32.MaxValue, StopWatch, FixedKey),

                new ProfilerSystem(TickType.LateUpdate, Int32.MinValue, StartWatch, LateKey),
                new ProfilerSystem(TickType.LateUpdate, Int32.MaxValue, StopWatch, LateKey),
            };

            runner.AddSystems(systems);
        }

        public void LogResults()
        {
            EntiCSLog.Log(GetLogableResults());
        }

        public string GetLogableResults()
        {
            _sb.Clear();
            foreach (var stopWatch in _stopWatches.Values)
            {
                _sb.AppendLine($"[{nameof(SystemsProfiler)}] {stopWatch.Key}:\t {stopWatch.GetAverageMs():F3}ms");
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

        private void StartWatch(string key)
        {
            if (!_settings.IsEnabled) return;
            _stopWatches[key].Start();
        }

        private void StopWatch(string key)
        {
            if (!_settings.IsEnabled) return;
            _stopWatches[key].Stop();
        }
    }
}

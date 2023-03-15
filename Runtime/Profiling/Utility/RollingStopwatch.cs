using System.Diagnostics;

namespace EntiCS.Profiling.Utility
{
    public class RollingStopwatch
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly DoubleValueTracker _valueTracker;

        public string Key => _valueTracker.Key;

        public RollingStopwatch(string key, int bufferLength)
        {
            _valueTracker = new DoubleValueTracker(key, bufferLength);
        }

        public void Start()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
            _valueTracker.AddValue(_stopwatch.Elapsed.TotalMilliseconds);
        }

        public double GetAverageMs()
        {
            return _valueTracker.GetAverage();
        }
    }
}

namespace EntiCS.Profiling.Utility
{
    public class DoubleValueTracker : ValueTracker<double>
    {
        public DoubleValueTracker(string key, int bufferLength)
            : base(key, bufferLength)
        { }

        public double GetAverage()
        {
            var sum = 0d;
            var sumCount = 0;

            for (var i = 0; i < _buffer.Length; i++)
            {
                var item = _buffer[i];
                if (item <= 0d)
                {
                    continue;
                }

                sum += item;
                sumCount++;
            }

            return sumCount > 0d ? sum / sumCount : -1d;
        }
    }
}

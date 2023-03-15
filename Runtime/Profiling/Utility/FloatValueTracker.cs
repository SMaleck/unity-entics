namespace EntiCS.Profiling.Utility
{
    public class FloatValueTracker : ValueTracker<float>
    {
        public FloatValueTracker(string key, int bufferLength)
            : base(key, bufferLength)
        { }

        public float GetAverage()
        {
            var sum = 0f;
            var sumCount = 0;

            for (var i = 0; i < _buffer.Length; i++)
            {
                var item = _buffer[i];
                if (item <= 0f)
                {
                    continue;
                }

                sum += item;
                sumCount++;
            }

            return sumCount > 0f ? sum / sumCount : -1f;
        }
    }
}

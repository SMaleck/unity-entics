namespace EntiCS.Profiling.Utility
{
    public class ValueTracker<T>
    {
        private int _bufferIndex = 0;

        // Do not change this to readonly.
        // This will cause not only the field but also the array content to become readonly
        public T[] _buffer;

        public string Key { get; }

        public ValueTracker(string key, int bufferLength)
        {
            Key = key;
            _buffer = new T[bufferLength];
        }

        public void AddValue(T value)
        {
            _buffer[_bufferIndex] = value;

            _bufferIndex = _bufferIndex == _buffer.Length - 1
                ? 0
                : _bufferIndex + 1;
        }
    }
}

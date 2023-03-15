namespace EntiCS.Profiling
{
    public class ProfilerSettings : IProfilerSettings
    {
        public bool IsEnabled => UnityEngine.Debug.isDebugBuild;
        public int DefaultBufferLength => 300;
    }
}

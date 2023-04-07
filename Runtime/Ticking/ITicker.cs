using System;

namespace EntiCS.Ticking
{
    public interface ITicker : IDisposable
    {
        bool IsPaused { get; }
        float TimeScale { get; }

        void SetIsPaused(bool isPaused);

        /// <summary>
        /// Sets the TimeScale, timeScale must be >= 0
        /// </summary>
        void SetTimeScale(float timeScale);

        void Add(TickType tickType, IUpdateable updateable);
        void Remove(TickType tickType, IUpdateable updateable);
    }
}
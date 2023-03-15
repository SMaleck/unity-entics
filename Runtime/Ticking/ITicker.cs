using System;
using static EntiCS.Ticking.EntiCSTicker;

namespace EntiCS.Ticking
{
    public interface ITicker : IDisposable
    {
        bool IsPaused { get; }
        float TimeScale { get; }

        public BeforeTick OnBeforeTick { get; set; }
        public AfterTick OnAfterTick { get; set; }

        void SetIsPaused(bool isPaused);

        /// <summary>
        /// Sets the TimeScale, timeScale must be >= 0
        /// </summary>
        void SetTimeScale(float timeScale);

        void AddUpdate(IUpdateable updateable);
        void AddFixedUpdate(IUpdateable updateable);
        void AddLateUpdate(IUpdateable updateable);

        void RemoveUpdate(IUpdateable updateable);
        void RemoveFixedUpdate(IUpdateable updateable);
        void RemoveLateUpdate(IUpdateable updateable);
    }
}
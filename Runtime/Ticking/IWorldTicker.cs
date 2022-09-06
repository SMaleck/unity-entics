using System;

namespace EntiCS.Ticking
{
    public interface IWorldTicker : IDisposable
    {
        bool IsPaused { get; }
        void SetIsPaused(bool isPaused);

        void Register(IUpdateable updateable);
        void Remove(IUpdateable updateable);
    }
}
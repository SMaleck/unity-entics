using System;
using System.Collections.Generic;
using Zenject;

namespace EntiCS.World
{
    public class WorldTicker : ITickable, IWorldTicker
    {
        private readonly List<IUpdateable> _updateables;
        private DateTime _lastUpdateAt;

        public bool IsPaused { get; private set; } = false;

        public WorldTicker([InjectLocal] TickableManager tickableManager)
        {
            _updateables = new List<IUpdateable>();
            ResetTimestamp();

            tickableManager.Add(this);
        }

        public void SetIsPaused(bool isPaused)
        {
            IsPaused = isPaused;

            if (!IsPaused)
            {
                ResetTimestamp();
            }
        }

        public void Register(IUpdateable updateable)
        {
            if (!_updateables.Contains(updateable))
            {
                _updateables.Add(updateable);
            }
        }

        public void Remove(IUpdateable updateable)
        {
            if (_updateables.Contains(updateable))
            {
                _updateables.Remove(updateable);
            }
        }

        void ITickable.Tick()
        {
            if (IsPaused)
            {
                return;
            }

            foreach (var updateable in _updateables)
            {
                var elapsedSeconds = FlushElapsedSeconds();
                updateable.Update(elapsedSeconds);
            }
        }

        private double FlushElapsedSeconds()
        {
            var now = DateTime.Now;
            var elapsedSeconds = (now - _lastUpdateAt).TotalSeconds;

            ResetTimestamp(now);

            return elapsedSeconds;
        }

        private void ResetTimestamp(DateTime now = default)
        {
            _lastUpdateAt = now == default
                ? DateTime.Now
                : now;
        }
    }
}

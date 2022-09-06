using System;
using System.Collections.Generic;
using UnityEngine;

namespace EntiCS.Ticking
{
    public class WorldTicker : IWorldTicker
    {
        private class WorldTickerComponent : MonoBehaviour
        {
            private Action _onUpdate;

            public void RegisterOnUpdate(Action onUpdate)
            {
                _onUpdate = onUpdate;
            }

            private void Update()
            {
                _onUpdate?.Invoke();
            }
        }

        private readonly List<IUpdateable> _updateables = new List<IUpdateable>();
        private readonly WorldTickerComponent _tickerComponent;

        public bool IsPaused { get; private set; } = false;

        public WorldTicker()
        {
            _tickerComponent = new GameObject().AddComponent<WorldTickerComponent>();
            _tickerComponent.RegisterOnUpdate(Tick);
        }

        public void SetIsPaused(bool isPaused)
        {
            IsPaused = isPaused;
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

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_tickerComponent);
        }

        private  void Tick()
        {
            if (IsPaused)
            {
                return;
            }

            foreach (var updateable in _updateables)
            {
                updateable.Update(Time.deltaTime);
            }
        }
    }
}

using EntiCS.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EntiCS.Ticking
{
    public class EntiCSTicker : ITicker
    {
        // The number is a bit arbitrary, but 3 is guaranteed for the systems channels
        // So double that seemed reasonable
        private const int InitialSize = 6;

        private readonly EntiCSTickerMonoProxy _tickerComponent;

        private readonly List<IUpdateable> _updateables = new List<IUpdateable>(InitialSize);
        private readonly List<IUpdateable> _fixedUpdateables = new List<IUpdateable>(InitialSize);
        private readonly List<IUpdateable> _lateUpdateables = new List<IUpdateable>(InitialSize);

        public bool IsPaused { get; private set; } = false;
        public float TimeScale { get; private set; } = 1f;

        public EntiCSTicker()
        {
            _tickerComponent = new GameObject(nameof(EntiCSTicker))
                .AddComponent<EntiCSTickerMonoProxy>();

            UnityEngine.Object.DontDestroyOnLoad(_tickerComponent);

            _tickerComponent.RegisterActions(OnUpdate, OnFixedUpdate, OnLateUpdate);
        }

        public void SetIsPaused(bool isPaused)
        {
            IsPaused = isPaused;
        }

        public void SetTimeScale(float timeScale)
        {
            TimeScale = Math.Max(0, timeScale);
        }

        public void Add(TickType tickType, IUpdateable updateable)
        {
            switch (tickType)
            {
                case TickType.Update:
                    Add(_updateables, updateable);
                    break;

                case TickType.FixedUpdate:
                    Add(_fixedUpdateables, updateable);
                    break;

                case TickType.LateUpdate:
                    Add(_lateUpdateables, updateable);
                    break;

                default:
                    EntiCSLog.Error($"cannot ADD {nameof(IUpdateable)} for TickType {tickType}");
                    break;
            }
        }

        public void Remove(TickType tickType, IUpdateable updateable)
        {
            switch (tickType)
            {
                case TickType.Update:
                    Remove(_updateables, updateable);
                    break;

                case TickType.FixedUpdate:
                    Remove(_fixedUpdateables, updateable);
                    break;

                case TickType.LateUpdate:
                    Remove(_lateUpdateables, updateable);
                    break;

                default:
                    EntiCSLog.Error($"cannot REMOVE {nameof(IUpdateable)} for TickType {tickType}");
                    break;
            }
        }

        public void Dispose()
        {
            _updateables.Clear();
            _fixedUpdateables.Clear();
            _lateUpdateables.Clear();

            UnityEngine.Object.Destroy(_tickerComponent.gameObject);
        }

        private static void Add(List<IUpdateable> updateables, IUpdateable updateable)
        {
            if (!updateables.Contains(updateable))
            {
                updateables.Add(updateable);
            }
        }

        private static void Remove(List<IUpdateable> updateables, IUpdateable updateable)
        {
            if (updateables.Contains(updateable))
            {
                updateables.Remove(updateable);
            }
        }

        private void OnUpdate()
        {
            Tick(_updateables, Time.unscaledDeltaTime, TickType.Update);
        }

        private void OnFixedUpdate()
        {
            Tick(_fixedUpdateables, Time.fixedDeltaTime, TickType.FixedUpdate);
        }

        private void OnLateUpdate()
        {
            Tick(_lateUpdateables, Time.unscaledDeltaTime, TickType.LateUpdate);
        }

        private void Tick(List<IUpdateable> updateables, float unscaledDeltaTime, TickType type)
        {
            if (IsPaused)
            {
                return;
            }

            var deltaTime = unscaledDeltaTime * TimeScale;
            for (var i = 0; i < updateables.Count; i++)
            {
                // Safeguard: Updateables might get added in the middle of execution
                if (updateables.Count <= i)
                {
                    return;
                }

                var updateable = updateables[i];
                updateable.OnUpdate(deltaTime);
            }
        }
    }
}

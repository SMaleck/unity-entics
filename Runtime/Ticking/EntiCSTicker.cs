using System;
using System.Collections.Generic;
using UnityEngine;

namespace EntiCS.Ticking
{
    public class EntiCSTicker : ITicker
    {
        private readonly EntiCSTickerMonoProxy _tickerComponent;

        private readonly List<IUpdateable> _updateables = new List<IUpdateable>();
        private readonly List<IUpdateable> _fixedUpdateables = new List<IUpdateable>();
        private readonly List<IUpdateable> _lateUpdateables = new List<IUpdateable>();

        public bool IsPaused { get; private set; } = false;
        public float TimeScale { get; private set; } = 1f;

        public delegate void BeforeTick(TickType tickType);
        public delegate void AfterTick(TickType updateType);

        public BeforeTick OnBeforeTick { get; set; }
        public AfterTick OnAfterTick { get; set; }

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
            TimeScale = Math.Max(0, TimeScale);
        }

        public void AddUpdate(IUpdateable updateable)
        {
            Add(_updateables, updateable);
        }

        public void AddFixedUpdate(IUpdateable updateable)
        {
            Add(_fixedUpdateables, updateable);
        }

        public void AddLateUpdate(IUpdateable updateable)
        {
            Add(_lateUpdateables, updateable);
        }

        public void RemoveUpdate(IUpdateable updateable)
        {
            Remove(_updateables, updateable);
        }

        public void RemoveFixedUpdate(IUpdateable updateable)
        {
            Remove(_fixedUpdateables, updateable);
        }

        public void RemoveLateUpdate(IUpdateable updateable)
        {
            Remove(_lateUpdateables, updateable);
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

            OnBeforeTick?.Invoke(type);

            var deltaTime = unscaledDeltaTime * TimeScale;
            for (var i = 0; i < updateables.Count; i++)
            {
                // Safeguard: Updateables might get added in the middle of execution
                if (updateables.Count <= i)
                {
                    return;
                }

                var updateable = updateables[i];
                updateable.Update(deltaTime);
            }

            OnAfterTick?.Invoke(type);
        }
    }
}

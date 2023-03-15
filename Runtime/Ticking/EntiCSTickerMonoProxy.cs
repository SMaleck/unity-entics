using System;
using UnityEngine;

namespace EntiCS.Ticking
{
    internal class EntiCSTickerMonoProxy : MonoBehaviour
    {
        private Action _onUpdate;
        private Action _onFixedUpdate;
        private Action _onLateUpdate;

        public void RegisterActions(
            Action onUpdate,
            Action onFixedUpdate,
            Action onLateUpdate)
        {
            _onUpdate = onUpdate;
            _onFixedUpdate = onFixedUpdate;
            _onLateUpdate = onLateUpdate;
        }

        private void Update()
        {
            _onUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            _onFixedUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            _onLateUpdate?.Invoke();
        }
    }
}

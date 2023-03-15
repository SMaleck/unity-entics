using EntiCS.Ticking;
using System;

namespace EntiCS.Utility
{
    public class UpdateableProxy : IUpdateable
    {
        private readonly Action<float> _onUpdate;

        public UpdateableProxy(Action<float> onUpdate)
        {
            _onUpdate = onUpdate;
        }

        public void Update(float elapsedSeconds)
        {
            _onUpdate.Invoke(elapsedSeconds);
        }
    }
}

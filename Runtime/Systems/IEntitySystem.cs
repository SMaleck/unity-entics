using System;

namespace EntiCS.Systems
{
    public interface IEntitySystem
    {
        int Priority { get; }
        Type[] Filter { get; }

        void Update(double elapsedSeconds, IEntity[] entities);
    }
}

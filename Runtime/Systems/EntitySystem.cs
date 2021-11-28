using System;

namespace EntiCS.Systems
{
    public abstract class EntitySystem : IEntitySystem
    {
        public virtual int Priority { get; } = 0;
        public abstract Type[] Filter { get; }

        public abstract void Update(double elapsedSeconds, IEntity[] entities);
    }
}

using System;
using System.Collections.Generic;
using EntiCS.Entities;
using EntiCS.Ticking;

namespace EntiCS.Systems
{
    public abstract class EntitySystem : IEntitySystem
    {
        public virtual TickType UpdateType { get; } = TickType.Update;
        public virtual int ExecutionOrder { get; } = 0;
        public abstract Type[] Filter { get; }

        public abstract void Update(float elapsedSeconds, IReadOnlyCollection<IEntity> entities);
    }
}

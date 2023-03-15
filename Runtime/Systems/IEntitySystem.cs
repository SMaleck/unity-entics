using System;
using System.Collections.Generic;
using EntiCS.Entities;
using EntiCS.Ticking;

namespace EntiCS.Systems
{
    public interface IEntitySystem
    {
        TickType UpdateType { get; }
        int ExecutionOrder { get; }
        Type[] Filter { get; }

        void Update(float elapsedSeconds, IReadOnlyCollection<IEntity> entities);
    }
}

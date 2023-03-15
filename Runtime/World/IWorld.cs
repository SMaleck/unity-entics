using EntiCS.Entities;
using System;
using System.Collections.Generic;

namespace EntiCS.World
{
    public interface IWorld : IDisposable
    {
        IReadOnlyCollection<IEntity> All { get; }

        void Add(IEntity entity);
        void Remove(IEntity entity);
        IReadOnlyCollection<IEntity> GetBy(Type[] filter);

        void ProcessEventQueue();
    }
}

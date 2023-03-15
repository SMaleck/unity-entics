using EntiCS.Entities;
using System;
using System.Collections.Generic;

namespace EntiCS.World
{
    internal interface IEntitiesRepository
    {
        IReadOnlyCollection<IEntity> All { get; }

        void Add(IEntity entity);
        void Remove(IEntity entity);
        IReadOnlyCollection<IEntity> GetBy(Type[] filter);
    }
}
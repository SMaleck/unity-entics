using System;

namespace EntiCS.Repositories
{
    internal interface IEntitiesRepository
    {
        void Register(IEntity entity);
        void Remove(IEntity entity);
        IEntity[] GetBy(Type[] filter);
    }
}
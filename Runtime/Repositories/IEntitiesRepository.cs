using System;

namespace EntiCS.Repositories
{
    public interface IEntitiesRepository
    {
        void Register(IEntity entity);
        void Remove(IEntity entity);
        IEntity[] GetBy(Type[] filter);
    }
}
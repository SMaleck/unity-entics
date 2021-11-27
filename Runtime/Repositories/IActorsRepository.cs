using System;

namespace EntiCS.Repositories
{
    public interface IActorsRepository
    {
        void Register(IActor actor);
        void Remove(IActor actor);
        IActor[] GetBy(Type[] filter);
    }
}
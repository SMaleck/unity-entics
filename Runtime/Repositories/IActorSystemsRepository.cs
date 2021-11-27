using System.Collections.Generic;
using EntiCS.Systems;

namespace EntiCS.Repositories
{
    public interface IActorSystemsRepository
    {
        IReadOnlyList<IActorSystem> All { get; }

        void Register(IActorSystem system);
        void Remove(IActorSystem system);
    }
}
using System.Collections.Generic;
using EntiCS.Systems;

namespace EntiCS.Repositories
{
    public interface IEntitySystemsRepository
    {
        IReadOnlyList<IEntitySystem> All { get; }

        void Register(IEntitySystem system);
        void Remove(IEntitySystem system);
    }
}
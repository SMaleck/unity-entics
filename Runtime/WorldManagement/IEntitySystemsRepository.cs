using System.Collections.Generic;
using EntiCS.Systems;
using EntiCS.Ticking;

namespace EntiCS.WorldManagement
{
    internal interface IEntitySystemsRepository
    {
        IReadOnlyDictionary<TickType, List<IEntitySystem>> All { get; }
        List<IEntitySystem> this[TickType type] { get; }

        void Add(IEntitySystem system);
        void AddRange(IEnumerable<IEntitySystem> systems);
        void Remove(IEntitySystem system);
    }
}
using System.Collections.Generic;
using System.Linq;
using EntiCS.Systems;
using EntiCS.Ticking;

namespace EntiCS.WorldManagement
{
    internal class EntitySystemsRepository : IEntitySystemsRepository
    {
        private readonly Dictionary<TickType, List<IEntitySystem>> _systems;
        public IReadOnlyDictionary<TickType, List<IEntitySystem>> All => _systems;

        public List<IEntitySystem> this[TickType type] => _systems[type];

        public EntitySystemsRepository()
        {
            _systems = new Dictionary<TickType, List<IEntitySystem>>()
            {
                { TickType.Update, new List<IEntitySystem>() },
                { TickType.FixedUpdate, new List<IEntitySystem>() },
                { TickType.LateUpdate, new List<IEntitySystem>() }
            };
        }

        public void Add(IEntitySystem system)
        {
            AddInternal(system, true);
        }

        public void AddRange(IEnumerable<IEntitySystem> systems)
        {
            foreach (var system in systems)
            {
                AddInternal(system, false);
            }

            foreach (var systemKvp in _systems)
            {
                Reorder(systemKvp.Value);
            }
        }

        private void AddInternal(IEntitySystem system, bool reorder)
        {
            var systems = _systems[system.UpdateType];
            if (!systems.Contains(system))
            {
                systems.Add(system);

                if (reorder)
                {
                    Reorder(systems);
                }
            }
        }

        public void Remove(IEntitySystem system)
        {
            var systems = _systems[system.UpdateType];
            if (systems.Contains(system))
            {
                systems.Remove(system);
                Reorder(systems);
            }
        }

        private static void Reorder(List<IEntitySystem> systems)
        {
            var ordered = systems
                .OrderBy(e => e.ExecutionOrder)
                .ToList();

            systems.Clear();
            systems.AddRange(ordered);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using EntiCS.Systems;

namespace EntiCS.Repositories
{
    internal class EntitySystemsRepository : IEntitySystemsRepository
    {
        private readonly List<IEntitySystem> _systems;
        public IReadOnlyList<IEntitySystem> All => _systems;

        public EntitySystemsRepository()
        {
            _systems = new List<IEntitySystem>();
        }

        public void Register(IEntitySystem system)
        {
            if (!_systems.Contains(system))
            {
                _systems.Add(system);
                Reorder();
            }
        }

        public void Remove(IEntitySystem system)
        {
            if (_systems.Contains(system))
            {
                _systems.Remove(system);
                Reorder();
            }
        }

        private void Reorder()
        {
            var ordered = _systems
                .OrderBy(e => e.Priority)
                .ToList();

            _systems.Clear();
            _systems.AddRange(ordered);
        }
    }
}

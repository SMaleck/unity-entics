using System.Collections.Generic;
using System.Linq;
using EntiCS.Systems;

namespace EntiCS.Repositories
{
    public class ActorSystemsRepository : IActorSystemsRepository
    {
        private readonly List<IActorSystem> _actorSystems;
        public IReadOnlyList<IActorSystem> All => _actorSystems;

        public ActorSystemsRepository()
        {
            _actorSystems = new List<IActorSystem>();
        }

        public void Register(IActorSystem system)
        {
            if (!_actorSystems.Contains(system))
            {
                _actorSystems.Add(system);
                Reorder();
            }
        }

        public void Remove(IActorSystem system)
        {
            if (_actorSystems.Contains(system))
            {
                _actorSystems.Remove(system);
                Reorder();
            }
        }

        private void Reorder()
        {
            var ordered = _actorSystems
                .OrderBy(e => e.Priority)
                .ToList();

            _actorSystems.Clear();
            _actorSystems.AddRange(ordered);
        }
    }
}

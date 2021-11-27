using System;
using EntiCS.Repositories;
using Zenject;

namespace EntiCS.World
{
    public class EnticsWorld : IUpdateable, IDisposable
    {
        public class Factory : PlaceholderFactory<EnticsWorld> { }

        private readonly IEntitiesRepository _entitiesRepository;
        private readonly IEntitySystemsRepository _systemsRepository;
        private readonly IWorldTicker _worldTicker;

        public EnticsWorld(
            IEntitiesRepository entitiesRepository,
            IEntitySystemsRepository systemsRepository,
            IWorldTicker worldTicker)
        {
            _entitiesRepository = entitiesRepository;
            _systemsRepository = systemsRepository;
            _worldTicker = worldTicker;

            _worldTicker.Register(this);
        }

        void IUpdateable.Update(double elapsedSeconds)
        {
            foreach (var system in _systemsRepository.All)
            {
                var actors = _entitiesRepository.GetBy(system.Filter);
                system.Update(elapsedSeconds, actors);
            }
        }

        void IDisposable.Dispose()
        {
            _worldTicker.Remove(this);
        }
    }
}

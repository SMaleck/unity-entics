using EntiCS.Repositories;
using EntiCS.Systems;
using System;
using Zenject;

namespace EntiCS.World
{
    public class EnticsWorld : IEnticsWorld, IUpdateable, IDisposable
    {
        public class Factory : PlaceholderFactory<EnticsWorld> { }

        private readonly IWorldTicker _worldTicker;
        private readonly IEntitiesRepository _entitiesRepository;
        private readonly IEntitySystemsRepository _systemsRepository;

        public EnticsWorld(IWorldTicker worldTicker)
        {
            _worldTicker = worldTicker;

            _entitiesRepository = new EntitiesRepository();
            _systemsRepository = new EntitySystemsRepository();

            _worldTicker.Register(this);
        }

        void IEnticsWorld.AddEntity(IEntity entity)
        {
            _entitiesRepository.Register(entity);
        }

        void IEnticsWorld.RemoveEntity(IEntity entity)
        {
            _entitiesRepository.Remove(entity);
        }

        void IEnticsWorld.AddSystem(IEntitySystem system)
        {
            _systemsRepository.Register(system);
        }

        void IEnticsWorld.RemoveSystem(IEntitySystem system)
        {
            _systemsRepository.Remove(system);
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

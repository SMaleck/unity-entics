using EntiCS.Repositories;
using EntiCS.Systems;
using EntiCS.Ticking;
using System;

namespace EntiCS.World
{
    public class EnticsWorld : IEnticsWorld, IUpdateable, IDisposable
    {
        private readonly IWorldTicker _worldTicker;
        private readonly IEntitiesRepository _entitiesRepository;
        private readonly IEntitySystemsRepository _systemsRepository;

        public bool IsPaused => _worldTicker.IsPaused;

        public EnticsWorld()
        {
            _worldTicker = new WorldTicker();
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

        public void SetIsPaused(bool isPaused)
        {
            _worldTicker.SetIsPaused(isPaused);
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
            _worldTicker.Dispose();
        }
    }
}

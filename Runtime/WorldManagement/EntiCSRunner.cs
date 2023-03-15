using EntiCS.Systems;
using EntiCS.Ticking;
using EntiCS.Utility;
using EntiCS.World;
using System;
using System.Collections.Generic;

namespace EntiCS.WorldManagement
{
    public class EntiCSRunner : IEntiCSRunner
    {
        private readonly IEntitySystemsRepository _systemsRepository;
        private readonly IUpdateable _updateProxy;
        private readonly IUpdateable _fixedUpdateProxy;
        private readonly IUpdateable _lateUpdateProxy;

        public IWorld World { get; }
        public ITicker Ticker { get; }

        public EntiCSRunner(IWorld world, ITicker ticker)
        {
            World = world;
            Ticker = ticker;

            _systemsRepository = new EntitySystemsRepository();

            _updateProxy = new UpdateableProxy(Update);
            _fixedUpdateProxy = new UpdateableProxy(FixedUpdate);
            _lateUpdateProxy = new UpdateableProxy(LateUpdate);

            Ticker.AddUpdate(_updateProxy);
            Ticker.AddFixedUpdate(_fixedUpdateProxy);
            Ticker.AddLateUpdate(_lateUpdateProxy);
        }

        public void AddSystem(IEntitySystem system)
        {
            _systemsRepository.Add(system);
        }

        public void AddSystems(IEnumerable<IEntitySystem> systems)
        {
            _systemsRepository.AddRange(systems);
        }

        public void RemoveSystem(IEntitySystem system)
        {
            _systemsRepository.Remove(system);
        }

        private void Update(float elapsedSeconds)
        {
            ExecuteUpdate(elapsedSeconds, TickType.Update);
        }

        private void FixedUpdate(float elapsedSeconds)
        {
            ExecuteUpdate(elapsedSeconds, TickType.FixedUpdate);
        }

        private void LateUpdate(float elapsedSeconds)
        {
            ExecuteUpdate(elapsedSeconds, TickType.LateUpdate);

            // Run cleanup tasks on world after everything else has been updated
            World.ProcessEventQueue();
        }

        private void ExecuteUpdate(float elapsedSeconds, TickType updateType)
        {
            var systems = _systemsRepository[updateType];
            for (var i = 0; i < systems.Count; i++)
            {
                var system = systems[i];
                var entities = World.GetBy(system.Filter);
                system.Update(elapsedSeconds, entities);
            }
        }

        void IDisposable.Dispose()
        {
            World.Dispose();

            Ticker.RemoveUpdate(_updateProxy);
            Ticker.RemoveFixedUpdate(_fixedUpdateProxy);
            Ticker.RemoveLateUpdate(_lateUpdateProxy);
        }
    }
}

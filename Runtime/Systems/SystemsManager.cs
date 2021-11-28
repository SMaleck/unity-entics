using EntiCS.World;
using System.Collections.Generic;
using Zenject;

namespace EntiCS.Systems
{
    internal class SystemsManager : IInitializable
    {
        private readonly IEnticsWorld _world;
        private readonly List<IEntitySystem> _systems;

        public SystemsManager(
            IEnticsWorld world,
            List<IEntitySystem> systems)
        {
            _world = world;
            _systems = systems;
        }

        public void Initialize()
        {
            foreach (var system in _systems)
            {
                _world.AddSystem(system);
            }
        }
    }
}

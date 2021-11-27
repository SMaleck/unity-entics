using System;
using EntiCS.Repositories;
using Zenject;

namespace EntiCS.Systems
{
    public abstract class EntitySystem : IEntitySystem
    {
        public virtual int Priority { get; } = 0;
        public abstract Type[] Filter { get; }

        [Inject]
        private void Inject(IEntitySystemsRepository systemsRepository)
        {
            systemsRepository.Register(this);
        }

        public abstract void Update(double elapsedSeconds, IEntity[] entities);
    }
}

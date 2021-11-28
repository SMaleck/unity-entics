using System;
using EntiCS.Components;
using EntiCS.Repositories;
using Zenject;

namespace EntiCS
{
    public class Entity : IEntity
    {
        public class Factory : PlaceholderFactory<Entity> { }

        private readonly ComponentsRepository _componentsRepo;

        public Entity()
        {
            _componentsRepo = new ComponentsRepository();
        }

        public T Get<T>() where T : IEntityComponent
        {
            return _componentsRepo.Get<T>();
        }

        public bool TryGet<T>(out T component) where T : IEntityComponent
        {
            return _componentsRepo.TryGet<T>(out component);
        }

        public bool Has<T>() where T : IEntityComponent
        {
            return _componentsRepo.Has<T>();
        }

        public bool Has(Type type)
        {
            return _componentsRepo.Has(type);
        }

        public IComponentsRepository Attach(IEntityComponent component)
        {
            _componentsRepo.Attach(component);
            return this;
        }

        public IComponentsRepository Remove(IEntityComponent component)
        {
            _componentsRepo.Remove(component);
            return this;
        }
    }
}

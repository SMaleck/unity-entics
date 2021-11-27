using System;
using EntiCS.Components;
using EntiCS.Repositories;
using Zenject;

namespace EntiCS
{
    public class Actor : IActor
    {
        public class Factory : PlaceholderFactory<Actor> { }

        private readonly ComponentsRepository _componentsRepo;

        public Actor(IActorsRepository actorsRepository)
        {
            _componentsRepo = new ComponentsRepository();
            actorsRepository.Register(this);
        }

        public T Get<T>() where T : IActorComponent
        {
            return _componentsRepo.Get<T>();
        }

        public bool TryGet<T>(out T component) where T : IActorComponent
        {
            return _componentsRepo.TryGet<T>(out component);
        }

        public bool Has<T>() where T : IActorComponent
        {
            return _componentsRepo.Has<T>();
        }

        public bool Has(Type type)
        {
            return _componentsRepo.Has(type);
        }

        public IComponentsRepository Attach(IActorComponent component)
        {
            _componentsRepo.Attach(component);
            return this;
        }

        public IComponentsRepository Remove(IActorComponent component)
        {
            _componentsRepo.Remove(component);
            return this;
        }
    }
}

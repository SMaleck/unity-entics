using System;
using EntiCS.Components;
using EntiCS.Repositories;
using UnityEngine;
using Zenject;

namespace EntiCS
{
    public class MonoActor : MonoBehaviour, IActor
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, MonoActor> { }

        private ComponentsRepository _componentsRepo;
        private IActorComponent[] _monoComponents;

        private void Awake()
        {
            TrySetupRepo();
        }

        protected void TrySetupRepo()
        {
            if (_componentsRepo != null)
            {
                return;
            }

            _componentsRepo = new ComponentsRepository();
            var components = GetMonoComponents();

            foreach (var component in components)
            {
                _componentsRepo.Attach(component);
            }
        }

        private IActorComponent[] GetMonoComponents()
        {
            return _monoComponents ?? (_monoComponents = GetComponentsInChildren<IActorComponent>());
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

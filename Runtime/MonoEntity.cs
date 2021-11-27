using System;
using EntiCS.Components;
using EntiCS.Repositories;
using UnityEngine;
using Zenject;

namespace EntiCS
{
    public class MonoEntity : MonoBehaviour, IEntity
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, MonoEntity> { }

        private ComponentsRepository _componentsRepo;
        private IEntityComponent[] _monoComponents;

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

        private IEntityComponent[] GetMonoComponents()
        {
            return _monoComponents ?? (_monoComponents = GetComponentsInChildren<IEntityComponent>());
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

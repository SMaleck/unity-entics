using EntiCS.Components;
using EntiCS.Repositories;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EntiCS
{
    public class MonoEntity : MonoBehaviour, IEntity
    {
        private ComponentsRepository _componentsRepo;

        public IReadOnlyList<IEntityComponent> Components => _componentsRepo.Components;

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
            var components = GetComponentsInChildren<IEntityComponent>();

            foreach (var component in components)
            {
                _componentsRepo.Attach(component);
            }
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

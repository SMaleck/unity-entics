using EntiCS.Entities.Components;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EntiCS.Entities
{
    /// <summary>
    /// A proxy to <see cref="Entity"/> that can be directly used on a GameObject
    /// </summary>
    public class MonoEntity : MonoBehaviour, IEntity
    {
        [SerializeReference] private List<IEntityComponent> _serializedComponents;

        private Entity _entityInternal;
        private Entity EntityInternal => _entityInternal ??= CreateEntityInternal();

        public IReadOnlyCollection<IEntityComponent> Components => EntityInternal.Components;

        private void Awake()
        {
            _entityInternal = CreateEntityInternal();
        }

        private Entity CreateEntityInternal()
        {
            if (_entityInternal != null)
            {
                return _entityInternal;
            }

            var entity = new Entity();

            var serialized = _serializedComponents ?? new List<IEntityComponent>();
            AttachComponents(serialized, entity);

            var attached = GetComponentsInChildren<IEntityComponent>();
            AttachComponents(attached, entity);

            return entity;
        }

        public T Get<T>() where T : IEntityComponent
        {
            return EntityInternal.Get<T>();
        }

        public bool TryGet<T>(out T component) where T : IEntityComponent
        {
            return EntityInternal.TryGet<T>(out component);
        }

        public bool Has<T>() where T : IEntityComponent
        {
            return EntityInternal.Has<T>();
        }

        public bool Has(Type type)
        {
            return EntityInternal.Has(type);
        }

        public IComponentsRepository Add(IEntityComponent component)
        {
            EntityInternal.Add(component);
            return this;
        }

        public IComponentsRepository Remove(IEntityComponent component)
        {
            EntityInternal.Remove(component);
            return this;
        }

        private static void AttachComponents(IReadOnlyList<IEntityComponent> components, IEntity entity)
        {
            for (var i = 0; i < components.Count; i++)
            {
                var component = components[i];
                entity.Add(component);
            }
        }

#if UNITY_EDITOR
        public IReadOnlyList<IEntityComponent> SerializedComponents => _serializedComponents ??= new List<IEntityComponent>();

        public void AddSerializedComponent(IEntityComponent component)
        {
            _serializedComponents ??= new List<IEntityComponent>();
            _serializedComponents.Add(component);

            OnValidate();
        }

        private void OnValidate()
        {
            foreach (var component in _serializedComponents)
            {
                if (component is EntityComponent entityComponent)
                {
                    entityComponent.UpdateEditorName();
                }
            }
        }
#endif
    }
}

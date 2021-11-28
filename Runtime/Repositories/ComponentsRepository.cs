using System;
using System.Collections.Generic;
using System.Linq;
using EntiCS.Components;

namespace EntiCS.Repositories
{
    internal class ComponentsRepository : IComponentsRepository
    {
        private readonly Dictionary<Type, IEntityComponent> _components;

        public ComponentsRepository()
        {
            _components = new Dictionary<Type, IEntityComponent>();
        }

        public T Get<T>() where T : IEntityComponent
        {
            if (!TryGet<T>(out var component))
            {
                throw new KeyNotFoundException($"No component of type [{(typeof(T).Name)}] found");
            }

            return component;
        }

        public bool TryGet<T>(out T component) where T : IEntityComponent
        {
            var result = _components.TryGetValue(typeof(T), out var actorComponent);
            component = result ? (T)actorComponent : default;

            return result;
        }

        public bool Has<T>() where T : IEntityComponent
        {
            return TryGet<T>(out _);
        }

        public bool Has(Type type)
        {
            return _components.Keys.Contains(type);
        }

        public IComponentsRepository Attach(IEntityComponent component)
        {
            var keyableTypes = GetKeyableTypes(component);
            foreach (var type in keyableTypes)
            {
                _components.Add(type, component);
            }

            return this;
        }

        public IComponentsRepository Remove(IEntityComponent component)
        {
            var keyableTypes = GetKeyableTypes(component);
            foreach (var type in keyableTypes)
            {
                _components.Remove(type);
            }

            return this;
        }

        private IReadOnlyList<Type> GetKeyableTypes(IEntityComponent component)
        {
            var types = component
                .GetType()
                .GetInterfaces()
                .Where(IsKeyableInterface)
                .ToArray();

            return types.Any()
                ? types
                : new[] { component.GetType() };
        }

        private bool IsKeyableInterface(Type type)
        {
            return !type.Name.Equals(nameof(IEntityComponent), StringComparison.InvariantCultureIgnoreCase) &&
                   typeof(IEntityComponent).IsAssignableFrom(type);
        }
    }
}

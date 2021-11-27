using System;
using System.Collections.Generic;
using System.Linq;
using EntiCS.Components;

namespace EntiCS.Repositories
{
    public class ComponentsRepository : IComponentsRepository
    {
        private readonly Dictionary<Type, IActorComponent> _components;

        public ComponentsRepository()
        {
            _components = new Dictionary<Type, IActorComponent>();
        }

        public T Get<T>() where T : IActorComponent
        {
            if (!TryGet<T>(out var component))
            {
                throw new KeyNotFoundException($"No component of type [{(typeof(T).Name)}] found");
            }

            return component;
        }

        public bool TryGet<T>(out T component) where T : IActorComponent
        {
            var result = _components.TryGetValue(typeof(T), out var actorComponent);
            component = result ? (T)actorComponent : default;

            return result;
        }

        public bool Has<T>() where T : IActorComponent
        {
            return TryGet<T>(out _);
        }

        public bool Has(Type type)
        {
            foreach (var component in _components.Values)
            {
                if (component.GetType().GetInterfaces().Contains(type))
                {
                    return true;
                }
            }

            return false;
        }

        public IComponentsRepository Attach(IActorComponent component)
        {
            var keyableTypes = GetKeyableTypes(component);
            foreach (var type in keyableTypes)
            {
                _components.Add(type, component);
            }

            return this;
        }

        public IComponentsRepository Remove(IActorComponent component)
        {
            var keyableTypes = GetKeyableTypes(component);
            foreach (var type in keyableTypes)
            {
                _components.Remove(type);
            }

            return this;
        }

        private IReadOnlyList<Type> GetKeyableTypes(IActorComponent component)
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
            return !type.Name.Equals(nameof(IActorComponent), StringComparison.InvariantCultureIgnoreCase) &&
                   typeof(IActorComponent).IsAssignableFrom(type);
        }
    }
}

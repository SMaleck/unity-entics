using EntiCS.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntiCS.Repositories
{
    internal class ComponentsRepository : IComponentsRepository
    {
        private readonly List<IEntityComponent> _componentsRaw =
            new List<IEntityComponent>();

        private readonly Dictionary<Type, List<IEntityComponent>> _componentsByType =
            new Dictionary<Type, List<IEntityComponent>>();

        public IReadOnlyList<IEntityComponent> Components => _componentsRaw;

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
            if (_componentsByType.TryGetValue(typeof(T), out var components))
            {
                component = components.Count > 0 ? (T)components[0] : default;
                return true;
            }

            component = default;
            return false;
        }

        public bool Has<T>() where T : IEntityComponent
        {
            return TryGet<T>(out _);
        }

        public bool Has(Type type)
        {
            return _componentsByType.Keys.Contains(type);
        }

        public IComponentsRepository Attach(IEntityComponent component)
        {
            _componentsRaw.Add(component);

            var keyableTypes = GetKeyableTypes(component);
            foreach (var type in keyableTypes)
            {
                if (_componentsByType.TryGetValue(type, out var components))
                {
                    components.Add(component);
                }
                else
                {
                    _componentsByType.Add(type, new List<IEntityComponent>() { component });
                }
            }

            return this;
        }

        public IComponentsRepository Remove(IEntityComponent component)
        {
            _componentsRaw.Remove(component);

            var keyableTypes = GetKeyableTypes(component);
            foreach (var type in keyableTypes)
            {
                if (_componentsByType.TryGetValue(type, out var components))
                {
                    components.Remove(component);
                }
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

using EntiCS.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntiCS.Entities
{
    internal class ComponentsRepository : IComponentsRepository
    {
        private readonly HashSet<IEntityComponent> _componentsRaw =
            new HashSet<IEntityComponent>();

        private readonly Dictionary<Type, List<IEntityComponent>> _componentsByType =
            new Dictionary<Type, List<IEntityComponent>>();

        public IReadOnlyCollection<IEntityComponent> Components => _componentsRaw;

        /// <summary>The first component of the specified type</summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        /// <returns>The first component of the specified type.</returns>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        public T Get<T>() where T : IEntityComponent
        {
            return (T)_componentsByType[typeof(T)][0];
        }

        public T[] GetAll<T>() where T : IEntityComponent
        {
            if (_componentsByType.TryGetValue(typeof(T), out var components))
            {
                var count = components.Count;
                var castComponents = new T[count];

                var i = 0;
                foreach (var component in components)
                {
                    castComponents[i] = (T)component;
                    i++;
                }

                return castComponents;
            }

            return Array.Empty<T>();
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
            return Has(typeof(T));
        }

        public bool Has(Type type)
        {
            return _componentsByType.ContainsKey(type);
        }

        public IComponentsRepository Add(IEntityComponent component)
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
                .Append(component.GetType())
                .ToArray();

            return types;
        }

        private bool IsKeyableInterface(Type type)
        {
            return !type.Name.Equals(nameof(IEntityComponent), StringComparison.InvariantCultureIgnoreCase) &&
                   typeof(IEntityComponent).IsAssignableFrom(type);
        }
    }
}

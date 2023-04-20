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

        private readonly Dictionary<Type, IEntityComponent> _componentsByType =
            new Dictionary<Type, IEntityComponent>();

        public IReadOnlyCollection<IEntityComponent> Components => _componentsRaw;

        /// <summary>The first component of the specified type</summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        /// <returns>The first component of the specified type.</returns>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        public T Get<T>() where T : IEntityComponent
        {
            return (T)_componentsByType[typeof(T)];
        }

        public bool TryGet<T>(out T component) where T : IEntityComponent
        {
            component = default;
            if (_componentsByType.TryGetValue(typeof(T), out var c))
            {
                component = (T)c;
                return true;
            }

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
            for (int i = 0; i < keyableTypes.Count; i++)
            {
                var type = keyableTypes[i];
                _componentsByType.Add(type, component);
            }

            return this;
        }

        public IComponentsRepository Remove(IEntityComponent component)
        {
            _componentsRaw.Remove(component);

            var keyableTypes = GetKeyableTypes(component);
            for (int i = 0; i < keyableTypes.Count; i++)
            {
                var type = keyableTypes[i];
                _componentsByType.Remove(type);
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

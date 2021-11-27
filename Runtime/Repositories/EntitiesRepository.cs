using System;
using System.Collections.Generic;
using System.Linq;

namespace EntiCS.Repositories
{
    public class EntitiesRepository : IEntitiesRepository
    {
        private readonly List<IEntity> _entities;
        private readonly Dictionary<Type[], IEntity[]> _filteredEntities;

        public EntitiesRepository()
        {
            _entities = new List<IEntity>();
            _filteredEntities = new Dictionary<Type[], IEntity[]>();
        }

        public void Register(IEntity entity)
        {
            if (!_entities.Contains(entity))
            {
                _entities.Add(entity);
                UpdateKnownFilters();
            }
        }

        public void Remove(IEntity entity)
        {
            if (_entities.Contains(entity))
            {
                _entities.Remove(entity);
                UpdateKnownFilters();
            }
        }

        public IEntity[] GetBy(Type[] filter)
        {
            if (!_filteredEntities.ContainsKey(filter))
            {
                AddFilter(filter);
            }

            return _filteredEntities[filter];
        }

        private void AddFilter(Type[] filter)
        {
            var entities = new List<IEntity>();
            foreach (var entity in _entities)
            {
                if (IsValidForFilter(entity, filter))
                {
                    entities.Add(entity);
                }
            }

            var actorsArray = entities.Count > 0
                ? _entities.ToArray()
                : Array.Empty<IEntity>();

            _filteredEntities.Add(filter, actorsArray);
        }

        private bool IsValidForFilter(IEntity entity, Type[] filter)
        {
            foreach (var item in filter)
            {
                if (!entity.Has(item))
                {
                    return false;
                }
            }

            return true;
        }

        private void UpdateKnownFilters()
        {
            var filters = _filteredEntities.Keys.ToArray();
            _filteredEntities.Clear();

            foreach (var filter in filters)
            {
                AddFilter(filter);
            }
        }
    }
}

using EntiCS.Entities;
using System;
using System.Collections.Generic;

namespace EntiCS.World
{
    internal class EntitiesRepository : IEntitiesRepository
    {
        private readonly HashSet<IEntity> _entities;
        private readonly Dictionary<Type[], EntityQueryResult> _queries;

        public IReadOnlyCollection<IEntity> All => _entities;

        public EntitiesRepository()
        {
            _entities = new HashSet<IEntity>();
            _queries = new Dictionary<Type[], EntityQueryResult>();
        }

        public void Add(IEntity entity)
        {
            if (!_entities.Contains(entity))
            {
                _entities.Add(entity);
                AddEntityToQuery(entity);
            }
        }

        public void Remove(IEntity entity)
        {
            if (_entities.Contains(entity))
            {
                _entities.Remove(entity);
                RemoveEntityFromQuery(entity);
            }
        }

        public IReadOnlyCollection<IEntity> GetBy(Type[] filter)
        {
            if (_queries.TryGetValue(filter, out var queryResult))
            {
                return queryResult.Entities;
            }

            return AddQuery(filter).Entities;
        }

        /// <summary>
        /// Adds a new filter and adds all applicable entities to it
        /// usually runs whenever a new filter is requested, i.e. a new System is introduced
        /// </summary>
        private EntityQueryResult AddQuery(Type[] filter)
        {
            var entities = new HashSet<IEntity>();

            foreach (var entity in _entities)
            {
                if (IsValidForFilter(entity, filter))
                {
                    entities.Add(entity);
                }
            }

            var queryResult = new EntityQueryResult(filter, entities);
            _queries.Add(filter, queryResult);

            return queryResult;
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

        /// <summary>
        /// Adds a new entity to all applicable filters
        /// </summary>
        private void AddEntityToQuery(IEntity entity)
        {
            foreach (var queryResult in _queries.Values)
            {
                if (IsValidForFilter(entity, queryResult.Filter) &&
                    !queryResult.Entities.Contains(entity))
                {
                    queryResult.Entities.Add(entity);
                }
            }
        }

        /// <summary>
        /// Removes an entity from all applicable filters
        /// </summary>
        private void RemoveEntityFromQuery(IEntity entity)
        {
            foreach (var queryResult in _queries.Values)
            {
                if (queryResult.Entities.Contains(entity))
                {
                    queryResult.Entities.Remove(entity);
                }
            }
        }
    }
}

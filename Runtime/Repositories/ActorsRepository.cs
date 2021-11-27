using System;
using System.Collections.Generic;
using System.Linq;

namespace EntiCS.Repositories
{
    public class ActorsRepository : IActorsRepository
    {
        private readonly List<IActor> _actors;
        private readonly Dictionary<Type[], IActor[]> _filteredActors;

        public ActorsRepository()
        {
            _actors = new List<IActor>();
            _filteredActors = new Dictionary<Type[], IActor[]>();
        }

        public void Register(IActor actor)
        {
            if (!_actors.Contains(actor))
            {
                _actors.Add(actor);
                UpdateKnownFilters();
            }
        }

        public void Remove(IActor actor)
        {
            if (_actors.Contains(actor))
            {
                _actors.Remove(actor);
                UpdateKnownFilters();
            }
        }

        public IActor[] GetBy(Type[] filter)
        {
            if (!_filteredActors.ContainsKey(filter))
            {
                AddFilter(filter);
            }

            return _filteredActors[filter];
        }

        private void AddFilter(Type[] filter)
        {
            var actors = new List<IActor>();
            foreach (var actor in _actors)
            {
                if (IsValidForFilter(actor, filter))
                {
                    actors.Add(actor);
                }
            }

            var actorsArray = actors.Count > 0
                ? _actors.ToArray()
                : Array.Empty<IActor>();

            _filteredActors.Add(filter, actorsArray);
        }

        private bool IsValidForFilter(IActor actor, Type[] filter)
        {
            foreach (var item in filter)
            {
                if (!actor.Has(item))
                {
                    return false;
                }
            }

            return true;
        }

        private void UpdateKnownFilters()
        {
            var filters = _filteredActors.Keys.ToArray();
            _filteredActors.Clear();

            foreach (var filter in filters)
            {
                AddFilter(filter);
            }
        }
    }
}

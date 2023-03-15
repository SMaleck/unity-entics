using EntiCS.Entities;
using System;
using System.Collections.Generic;

namespace EntiCS.World
{
    public class EntityQueryResult
    {
        public Type[] Filter { get; }
        public HashSet<IEntity> Entities { get; }

        public EntityQueryResult(Type[] filter, HashSet<IEntity> entities)
        {
            Filter = filter;
            Entities = entities;
        }
    }
}

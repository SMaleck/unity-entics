using EntiCS.Entities;
using EntiCS.Utility;
using System;
using System.Collections.Generic;

namespace EntiCS.World
{
    public class EntiCSWorld : IWorld, IDisposable
    {
        private readonly IEntitiesRepository _entitiesRepository;
        private readonly Queue<StructureEvent> _eventQueue;

        public IReadOnlyCollection<IEntity> All => _entitiesRepository.All;

        public EntiCSWorld()
        {
            _entitiesRepository = new EntitiesRepository();
            _eventQueue = new Queue<StructureEvent>();
        }

        public void Add(IEntity entity)
        {
            _eventQueue.Enqueue(new StructureEvent(
                StructureEventOperation.AddEntity, entity));
        }

        public void Remove(IEntity entity)
        {
            _eventQueue.Enqueue(new StructureEvent(
                StructureEventOperation.RemoveEntity, entity));
        }

        public IReadOnlyCollection<IEntity> GetBy(Type[] filter)
        {
            return _entitiesRepository.GetBy(filter);
        }

        public void ProcessEventQueue()
        {
            var count = _eventQueue.Count;
            for (var i = 0; i < count; i++)
            {
                var worldEvent = _eventQueue.Dequeue();

                switch (worldEvent.Operation)
                {
                    case StructureEventOperation.AddEntity:
                        _entitiesRepository.Add(worldEvent.Entity);
                        break;

                    case StructureEventOperation.RemoveEntity:
                        _entitiesRepository.Remove(worldEvent.Entity);
                        break;

                    default:
                        EntiCSLog.Error($"Unknown Operation: {worldEvent.Operation}");
                        break;
                }
            }
        }

        void IDisposable.Dispose()
        {
            // ToDo Destroy all entities
        }
    }
}

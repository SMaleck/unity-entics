using EntiCS.Entities;

namespace EntiCS.World
{
    public enum StructureEventOperation
    {
        AddEntity = 0,
        RemoveEntity = 1
    } 

    public class StructureEvent
    {
        public StructureEventOperation Operation { get; }
        public IEntity Entity { get; }

        public StructureEvent(StructureEventOperation operation, IEntity entity)
        {
            Operation = operation;
            Entity = entity;
        }
    }
}

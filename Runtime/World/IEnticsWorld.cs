using EntiCS.Systems;

namespace EntiCS.World
{
    public interface IEnticsWorld
    {
        void AddEntity(IEntity entity);
        void RemoveEntity(IEntity entity);

        void AddSystem(IEntitySystem system);
        void RemoveSystem(IEntitySystem system);
    }
}

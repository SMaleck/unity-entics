namespace EntiCS.World
{
    public interface IWorldTicker
    {
        bool IsPaused { get; }
        void SetIsPaused(bool isPaused);

        void Register(IUpdateable updateable);
        void Remove(IUpdateable updateable);
    }
}
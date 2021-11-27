using System;

namespace EntiCS.Systems
{
    public interface IActorSystem
    {
        int Priority { get; }
        Type[] Filter { get; }

        void Update(double elapsedSeconds, IActor[] actors);
    }
}

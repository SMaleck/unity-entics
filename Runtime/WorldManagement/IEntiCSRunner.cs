using EntiCS.Systems;
using EntiCS.Ticking;
using EntiCS.World;
using System;
using System.Collections.Generic;

namespace EntiCS.WorldManagement
{
    public interface IEntiCSRunner : IDisposable
    {
        IWorld World { get; }
        ITicker Ticker { get; }

        void AddSystem(IEntitySystem system);
        void AddSystems(IEnumerable<IEntitySystem> systems);
        void RemoveSystem(IEntitySystem system);
    }
}
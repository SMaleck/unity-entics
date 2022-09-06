using EntiCS.Components;
using System;
using System.Collections.Generic;

namespace EntiCS.Repositories
{
    public interface IComponentsRepository
    {
        IReadOnlyList<IEntityComponent> Components { get; }

        T Get<T>() where T : IEntityComponent;
        bool TryGet<T>(out T component) where T : IEntityComponent;
        bool Has<T>() where T : IEntityComponent;
        bool Has(Type type);

        IComponentsRepository Attach(IEntityComponent component);
        IComponentsRepository Remove(IEntityComponent component);
    }
}
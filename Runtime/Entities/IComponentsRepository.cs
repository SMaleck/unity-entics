using EntiCS.Entities.Components;
using System;
using System.Collections.Generic;

namespace EntiCS.Entities
{
    public interface IComponentsRepository
    {
        IReadOnlyCollection<IEntityComponent> Components { get; }

        T Get<T>() where T : IEntityComponent;
        bool TryGet<T>(out T component) where T : IEntityComponent;
        bool Has<T>() where T : IEntityComponent;
        bool Has(Type type);

        IComponentsRepository Add(IEntityComponent component);
        IComponentsRepository Remove(IEntityComponent component);
    }
}
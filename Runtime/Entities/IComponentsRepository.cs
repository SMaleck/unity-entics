using System;
using System.Collections.Generic;
using EntiCS.Entities.Components;

namespace EntiCS.Entities
{
    public interface IComponentsRepository
    {
        IReadOnlyCollection<IEntityComponent> Components { get; }

        T Get<T>() where T : IEntityComponent;
        T[] GetAll<T>() where T : IEntityComponent;
        bool TryGet<T>(out T component) where T : IEntityComponent;
        bool Has<T>() where T : IEntityComponent;
        bool Has(Type type);

        IComponentsRepository Add(IEntityComponent component);
        IComponentsRepository Remove(IEntityComponent component);
    }
}
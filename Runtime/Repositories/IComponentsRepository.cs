using System;
using EntiCS.Components;

namespace EntiCS.Repositories
{
    public interface IComponentsRepository
    {
        T Get<T>() where T : IActorComponent;
        bool TryGet<T>(out T component) where T : IActorComponent;
        bool Has<T>() where T : IActorComponent;
        bool Has(Type type);

        IComponentsRepository Attach(IActorComponent component);
        IComponentsRepository Remove(IActorComponent component);
    }
}
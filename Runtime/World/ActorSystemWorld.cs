using System;
using EntiCS.Repositories;
using Zenject;

namespace EntiCS.World
{
    public class ActorSystemWorld : IUpdateable, IDisposable
    {
        public class Factory : PlaceholderFactory<ActorSystemWorld> { }

        private readonly IActorsRepository _actorsRepository;
        private readonly IActorSystemsRepository _actorSystemsRepository;
        private readonly IWorldTicker _worldTicker;

        public ActorSystemWorld(
            IActorsRepository actorsRepository,
            IActorSystemsRepository actorSystemsRepository,
            IWorldTicker worldTicker)
        {
            _actorsRepository = actorsRepository;
            _actorSystemsRepository = actorSystemsRepository;
            _worldTicker = worldTicker;

            _worldTicker.Register(this);
        }

        void IUpdateable.Update(double elapsedSeconds)
        {
            foreach (var system in _actorSystemsRepository.All)
            {
                var actors = _actorsRepository.GetBy(system.Filter);
                system.Update(elapsedSeconds, actors);
            }
        }

        void IDisposable.Dispose()
        {
            _worldTicker.Remove(this);
        }
    }
}

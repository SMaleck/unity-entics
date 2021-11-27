using EntiCS.Repositories;
using EntiCS.World;
using Zenject;

namespace EntiCS.Installation
{
    public class ActorSystemWorldInstaller : Installer<ActorSystemWorldInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ActorsRepository>().AsSingle();
            Container.BindInterfacesTo<ActorSystemsRepository>().AsSingle();
            Container.Bind<IWorldTicker>().To<WorldTicker>().AsSingle();

            Container.BindInterfacesTo<ActorSystemWorld>().AsSingle().NonLazy();
        }
    }
}

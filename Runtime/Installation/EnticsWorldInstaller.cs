using EntiCS.Repositories;
using EntiCS.World;
using Zenject;

namespace EntiCS.Installation
{
    public class EnticsWorldInstaller : Installer<EnticsWorldInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EntitiesRepository>().AsSingle();
            Container.BindInterfacesTo<EntitySystemsRepository>().AsSingle();
            Container.Bind<IWorldTicker>().To<WorldTicker>().AsSingle();

            Container.BindInterfacesTo<EnticsWorld>().AsSingle().NonLazy();
        }
    }
}

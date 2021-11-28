using EntiCS.Systems;
using EntiCS.World;
using Zenject;

namespace EntiCS.Installation
{
    public class EnticsWorldInstaller : Installer<EnticsWorldInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SystemsManager>().AsSingle().NonLazy();

            Container.Bind<IWorldTicker>().To<WorldTicker>().AsSingle();
            Container.BindInterfacesTo<EnticsWorld>().AsSingle().NonLazy();
        }
    }
}

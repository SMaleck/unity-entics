using Zenject;

namespace EntiCS.Installation
{
    public class EnticsFactoryInstaller : Installer<EnticsFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<
                    Entity,
                    Entity.Factory>()
                .AsSingle();

            Container.BindFactory<
                    UnityEngine.Object, 
                    MonoEntity, 
                    MonoEntity.Factory>()
                .FromFactory<PrefabFactory<MonoEntity>>();
        }
    }
}

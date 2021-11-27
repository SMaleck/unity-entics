using Zenject;

namespace EntiCS.Installation
{
    public class ActorSystemFactoryInstaller : Installer<ActorSystemFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<
                    Actor,
                    Actor.Factory>()
                .AsSingle();

            Container.BindFactory<
                    UnityEngine.Object, 
                    MonoActor, 
                    MonoActor.Factory>()
                .FromFactory<PrefabFactory<MonoActor>>();
        }
    }
}

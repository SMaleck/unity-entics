using EntiCS.Creation;
using EntiCS.World;
using Zenject;

namespace EntiCS
{
    public class StandaloneMonoEntity : MonoEntity
    {
        [Inject]
        private void Inject(
            IStandaloneEntityFactory standaloneActorFactory,
            IEnticsWorld world)
        {
            TrySetupRepo();

            standaloneActorFactory.SetupStandalone(this);
            world.AddEntity(this);
        }
    }
}

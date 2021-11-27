using EntiCS.Creation;
using EntiCS.Repositories;
using Zenject;

namespace EntiCS
{
    public class StandaloneMonoEntity : MonoEntity
    {
        [Inject]
        private void Inject(
            IStandaloneEntityFactory standaloneActorFactory,
            IEntitiesRepository entitiesRepository)
        {
            TrySetupRepo();
            standaloneActorFactory.SetupStandalone(this);
            entitiesRepository.Register(this);
        }
    }
}

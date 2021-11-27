using EntiCS.Creation;
using EntiCS.Repositories;
using Zenject;

namespace EntiCS
{
    public class StandaloneMonoActor : MonoActor
    {
        [Inject]
        private void Inject(
            IStandaloneActorFactory standaloneActorFactory,
            IActorsRepository actorsRepository)
        {
            TrySetupRepo();
            standaloneActorFactory.SetupStandalone(this);
            actorsRepository.Register(this);
        }
    }
}

using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameStateMachine>().AsSingle();
            
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<MainMenuState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GamePlayState>().AsSingle();
        }
    }
}
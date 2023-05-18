using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.SceneLoading;
using Infrastructure.Services;
using Infrastructure.Services.Logger;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private ScenesInfo _scenesInfo;

        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindServices();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>().AsSingle();

            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<MainMenuState>().AsSingle();
            Container.Bind<LoadSceneState>().AsSingle();
            Container.Bind<GameplayState>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<SceneLoader>().AsSingle();
            
            Container
                .Bind<ICustomLogger>()
                .To<CustomLogger>()
                .AsSingle();
            
            Container
                .Bind<UtilityDataProvider>()
                .AsSingle()
                .WithArguments(_scenesInfo);
        }
    }
}
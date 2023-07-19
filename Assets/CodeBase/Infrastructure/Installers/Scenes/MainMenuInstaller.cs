using Infrastructure.SceneServices.SceneServicesSetting;
using Infrastructure.SceneServices.WarmUppers;
using Infrastructure.SceneServices.WorldObjectsSpawners;
using Zenject;

namespace Infrastructure.Installers.Scenes
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            BindSceneServices();

        private void BindSceneServices()
        {
            Container.BindInterfacesTo<MainMenuServicesSetter>().AsSingle();
            
            Container.Bind<IWarmUpper>().To<MainMenuWarmUpper>().AsSingle();
            Container.Bind<IWorldObjectsSpawner>().To<MainMenuWorldObjectsSpawner>().AsSingle();
        }
    }
}
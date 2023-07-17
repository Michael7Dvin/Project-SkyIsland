using Infrastructure.LevelLoading.SceneServices.WarmUppers.MainMenu;
using Infrastructure.LevelLoading.SceneServices.WorldObjectsSpawners.MainMenu;
using Zenject;

namespace Infrastructure.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            BindLoadingServices();

        private void BindLoadingServices()
        {
            Container.BindInterfacesAndSelfTo<MainMenuWarmUpper>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuWorldObjectsSpawner>().AsSingle();
        }
    }
}
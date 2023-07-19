using Zenject;

namespace Infrastructure.Installers.Scenes
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            Container.BindInterfacesTo<Bootstrapper>().AsSingle();
    }
}

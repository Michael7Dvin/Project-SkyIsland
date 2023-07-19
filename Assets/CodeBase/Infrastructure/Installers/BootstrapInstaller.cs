using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            Container.BindInterfacesTo<Bootstrapper>().AsSingle();
    }
}

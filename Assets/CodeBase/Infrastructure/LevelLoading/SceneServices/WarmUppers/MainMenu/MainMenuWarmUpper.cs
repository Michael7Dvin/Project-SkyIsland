using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading.SceneServices.SceneServicesProviding;
using UI.Services.Factories.Utilities;
using UI.Services.Factories.Window;

namespace Infrastructure.LevelLoading.SceneServices.WarmUppers.MainMenu
{
    public class MainMenuWarmUpper : IWarmUpper
    {
        private readonly IUiUtilitiesFactory _uiUtilitiesFactory;
        private readonly IWindowFactory _windowFactory;

        private readonly ISceneServicesProvider _sceneServicesProvider;

        public MainMenuWarmUpper(IUiUtilitiesFactory uiUtilitiesFactory,
            IWindowFactory windowFactory,
            ISceneServicesProvider sceneServicesProvider)
        {
            _uiUtilitiesFactory = uiUtilitiesFactory;
            _windowFactory = windowFactory;
            _sceneServicesProvider = sceneServicesProvider;
        }

        public void Initialize() => 
            SetSelfToProvider();

        public async UniTask WarmUp()
        {
            await _uiUtilitiesFactory.WarmUp();
            await _windowFactory.WarmUp();
        }

        private void SetSelfToProvider() => 
            _sceneServicesProvider.SetWarmUpper(this);
    }
}
using Cysharp.Threading.Tasks;
using Gameplay.Services.Factories.Heros;
using Gameplay.Services.Factories.PlayerCameras;
using Infrastructure.LevelLoading.SceneServices.SceneServicesProviding;
using UI.Services.Factories.Utilities;
using UI.Services.Factories.Window;

namespace Infrastructure.LevelLoading.SceneServices.WarmUppers.Island
{
    public class IslandWarmUpper : IWarmUpper
    {
        private readonly IHeroFactory _heroFactory;
        private readonly IPlayerCameraFactory _playerCameraFactory;
        private readonly IUiUtilitiesFactory _iuiUtilitiesFactory;
        private readonly IWindowFactory _windowFactory;

        private readonly ISceneServicesProvider _sceneServicesProvider;

        public IslandWarmUpper(IHeroFactory heroFactory,
            IPlayerCameraFactory playerCameraFactory,
            IUiUtilitiesFactory iuiUtilitiesFactory,
            IWindowFactory windowFactory,
            ISceneServicesProvider sceneServicesProvider)
        {
            _heroFactory = heroFactory;
            _playerCameraFactory = playerCameraFactory;
            _iuiUtilitiesFactory = iuiUtilitiesFactory;
            _windowFactory = windowFactory;
            
            _sceneServicesProvider = sceneServicesProvider;
        }

        public void Initialize() => 
            SetSelfToProvider();

        public async UniTask WarmUp()
        {
            await _heroFactory.WarmUp();
            await _playerCameraFactory.WarmUp();
            await _iuiUtilitiesFactory.WarmUp();
            await _windowFactory.WarmUp();
        }

        private void SetSelfToProvider() => 
            _sceneServicesProvider.SetWarmUpper(this);
    }
}
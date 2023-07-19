using Cysharp.Threading.Tasks;
using Gameplay.Services.Factories.Heros;
using Gameplay.Services.Factories.PlayerCameras;
using UI.Services.Factories.Utilities;
using UI.Services.Factories.Window;

namespace Infrastructure.SceneServices.WarmUppers
{
    public class IslandWarmUpper : IWarmUpper
    {
        private readonly IHeroFactory _heroFactory;
        private readonly IPlayerCameraFactory _playerCameraFactory;
        private readonly IUiUtilitiesFactory _iuiUtilitiesFactory;
        private readonly IWindowFactory _windowFactory;

        public IslandWarmUpper(IHeroFactory heroFactory,
            IPlayerCameraFactory playerCameraFactory,
            IUiUtilitiesFactory iuiUtilitiesFactory,
            IWindowFactory windowFactory)
        {
            _heroFactory = heroFactory;
            _playerCameraFactory = playerCameraFactory;
            _iuiUtilitiesFactory = iuiUtilitiesFactory;
            _windowFactory = windowFactory;
        }

        public async UniTask WarmUp()
        {
            await _heroFactory.WarmUp();
            await _playerCameraFactory.WarmUp();
            await _iuiUtilitiesFactory.WarmUp();
            await _windowFactory.WarmUp();
        }
    }
}
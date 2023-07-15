using Cysharp.Threading.Tasks;
using Gameplay.Services.Creation.Heros.Factory;
using Gameplay.Services.Creation.PlayerCamera;
using Infrastructure.LevelLoading.LevelServicesProviding;
using UI.Services.Factories.UI;
using UI.Services.Factories.Window;

namespace Infrastructure.LevelLoading.WarmUpping
{
    public class IslandWarmUpper : IWarmUpper
    {
        private readonly IHeroFactory _heroFactory;
        private readonly IPlayerCameraFactory _playerCameraFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IWindowFactory _windowFactory;

        private readonly ILevelServicesProvider _levelServicesProvider;

        public IslandWarmUpper(IHeroFactory heroFactory,
            IPlayerCameraFactory playerCameraFactory,
            IUIFactory uiFactory,
            IWindowFactory windowFactory,
            ILevelServicesProvider levelServicesProvider)
        {
            _heroFactory = heroFactory;
            _playerCameraFactory = playerCameraFactory;
            _uiFactory = uiFactory;
            _windowFactory = windowFactory;
            
            _levelServicesProvider = levelServicesProvider;
        }

        public void Initialize() => 
            SetSelfToProvider();

        public async UniTask WarmUp()
        {
            await _heroFactory.WarmUp();
            await _playerCameraFactory.WarmUp();
            await _uiFactory.WarmUp();
            await _windowFactory.WarmUp();
        }

        private void SetSelfToProvider() => 
            _levelServicesProvider.SetWarmUpper(this);
    }
}
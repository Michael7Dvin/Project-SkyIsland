using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading.SceneServices.SceneServicesProviding;
using UI.Services.Operating;
using UI.Services.Spawners;
using UI.Windows;

namespace Infrastructure.LevelLoading.SceneServices.WorldObjectsSpawners.MainMenu
{
    public class MainMenuWorldObjectsSpawner : IWorldObjectsSpawner
    {
        private readonly ISceneServicesProvider _sceneServicesProvider;
        private readonly IUiUtilitiesSpawner _uiUtilitiesSpawner;
        private readonly IWindowsOperator _windowsOperator;

        public MainMenuWorldObjectsSpawner(ISceneServicesProvider sceneServicesProvider, IUiUtilitiesSpawner uiUtilitiesSpawner, IWindowsOperator windowsOperator)
        {
            _sceneServicesProvider = sceneServicesProvider;
            _uiUtilitiesSpawner = uiUtilitiesSpawner;
            _windowsOperator = windowsOperator;
        }

        public void Initialize() => 
            SetSelfToProvider();

        public async UniTask SpawnWorldObjects()
        {
            await _uiUtilitiesSpawner.SpawnCanvas();
            await _uiUtilitiesSpawner.SpawnEventSystem();
            await _windowsOperator.OpenWindow(WindowType.MainMenu);
        }

        private void SetSelfToProvider() => 
            _sceneServicesProvider.SetWorldObjectsSpawner(this);
    }
}
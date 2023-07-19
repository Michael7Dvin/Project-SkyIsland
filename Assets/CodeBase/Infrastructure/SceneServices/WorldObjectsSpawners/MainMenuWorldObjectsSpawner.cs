using Cysharp.Threading.Tasks;
using UI.Services.Operating;
using UI.Services.Spawners;
using UI.Windows;

namespace Infrastructure.SceneServices.WorldObjectsSpawners
{
    public class MainMenuWorldObjectsSpawner : IWorldObjectsSpawner
    {
        private readonly IUiUtilitiesSpawner _uiUtilitiesSpawner;
        private readonly IWindowsOperator _windowsOperator;

        public MainMenuWorldObjectsSpawner(IUiUtilitiesSpawner uiUtilitiesSpawner, IWindowsOperator windowsOperator)
        {
            _uiUtilitiesSpawner = uiUtilitiesSpawner;
            _windowsOperator = windowsOperator;
        }
        
        public async UniTask SpawnWorldObjects()
        {
            await _uiUtilitiesSpawner.SpawnCanvas();
            await _uiUtilitiesSpawner.SpawnEventSystem();
            await _windowsOperator.OpenWindow(WindowType.MainMenu);
        }
    }
}
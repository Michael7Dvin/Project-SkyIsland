using Common.FSM;
using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading.SceneServices.SceneServicesProviding;
using Infrastructure.LevelLoading.SceneServices.WarmUppers;
using Infrastructure.LevelLoading.SceneServices.WorldObjectsSpawners;
using Infrastructure.Services.Input;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.GameFSM.States
{
    public class MainMenuState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly ISceneServicesProvider _sceneServicesProvider;
        private readonly IInputService _inputService;

        public MainMenuState(ISceneLoader sceneLoader,
            IAddressablesLoader addressablesLoader,
            ISceneServicesProvider sceneServicesProvider,
            IInputService inputService)
        {
            _sceneLoader = sceneLoader;
            _addressablesLoader = addressablesLoader;
            _sceneServicesProvider = sceneServicesProvider;
            _inputService = inputService;
        }

        public async void Enter()
        {
            await _sceneLoader.Load(SceneID.MainMenu);

            await WarmUp();
            await SpawnWorldObjects();
            
            _inputService.UI.Enable();
        }

        public void Exit()
        {
            _inputService.UI.Disable();
            _addressablesLoader.ClearCache();
        }
        
        private async UniTask WarmUp()
        {
            IWarmUpper warmUpper = await _sceneServicesProvider.GetWarmUpper();
            await warmUpper.WarmUp();
        }

        private async UniTask SpawnWorldObjects()
        {
            IWorldObjectsSpawner worldObjectsSpawner = await _sceneServicesProvider.GetWorldObjectsSpawner();
            await worldObjectsSpawner.SpawnWorldObjects();
        }
    }
}
using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.UI;
using Infrastructure.Services.Instantiating;
using UI.Services.Mediating;
using UI.Windows.Implementations;
using UnityEngine;

namespace UI.Windows.Factory
{
    public class WindowFactory : IWindowFactory
    {
        private Canvas _canvas;

        private readonly IUIAssetsProvider _uiAssetsProvider;

        private readonly IInstantiator _instantiator;
        private IMediator _mediator;

        public WindowFactory(IUIAssetsProvider uiAssetsProvider, IInstantiator instantiator)
        {
            _uiAssetsProvider = uiAssetsProvider;
            _instantiator = instantiator;
        }
        
        public void Init(IMediator mediator) => 
            _mediator = mediator;

        public async UniTask WarmUp()
        {
            await _uiAssetsProvider.LoadMainMenuWindow();
            await _uiAssetsProvider.LoadSaveSelectionWindow();
            await _uiAssetsProvider.LoadPauseWindow();
            await _uiAssetsProvider.LoadDeathWindow();
        }

        public void ResetCanvas(Canvas canvas) => 
            _canvas = canvas;

        public async UniTask<MainMenuWindow> CreateMainMenuWindow()
        {
            MainMenuWindow prefab = await _uiAssetsProvider.LoadMainMenuWindow();
            
            MainMenuWindow mainMenuWindow = _instantiator.Instantiate(prefab, _canvas.transform);
            mainMenuWindow.Construct(_mediator);

            return mainMenuWindow;
        }

        public async UniTask<SaveSelectionWindow> CreateSaveSelectionWindow()
        {
            SaveSelectionWindow prefab = await _uiAssetsProvider.LoadSaveSelectionWindow();
            
            SaveSelectionWindow saveSelectionWindow = _instantiator.Instantiate(prefab, _canvas.transform);
            saveSelectionWindow.Construct(_mediator);

            return saveSelectionWindow;
        }

        public async UniTask<PauseWindow> CreatePauseWindow()
        {
            PauseWindow prefab = await _uiAssetsProvider.LoadPauseWindow();
            
            PauseWindow pauseWindow = _instantiator.Instantiate(prefab, _canvas.transform);
            pauseWindow.Construct(_mediator);

            return pauseWindow;
        }

        public async UniTask<DeathWindow> CreateDeathWindow()
        {
            DeathWindow prefab = await _uiAssetsProvider.LoadDeathWindow();
            
            DeathWindow deathWindow = _instantiator.Instantiate(prefab, _canvas.transform);
            deathWindow.Construct(_mediator);

            return deathWindow;
        }
    }
}
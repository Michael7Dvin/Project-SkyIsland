using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.UI;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.StaticDataProviding;
using UI.Services.Mediating;
using UI.Windows.Implementations.DeathWindow;
using UI.Windows.Implementations.MainMenu;
using UI.Windows.Implementations.PauseWindow;
using UI.Windows.Implementations.SaveSelection;
using UnityEngine;

namespace UI.Windows.Factory
{
    public class WindowFactory : IWindowFactory
    {
        private Canvas _canvas;

        private readonly IUIAssetsProvider _uiAssetsProvider;

        private readonly IInstantiator _instantiator;
        private readonly UIConfig _uiConfig;
        
        private IMediator _mediator;

        public WindowFactory(IUIAssetsProvider uiAssetsProvider,
            IInstantiator instantiator,
            IStaticDataProvider staticDataProvider)
        {
            _uiAssetsProvider = uiAssetsProvider;
            _instantiator = instantiator;
            _uiConfig = staticDataProvider.UIConfig;
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
            MainMenuWindowView viewPrefab = await _uiAssetsProvider.LoadMainMenuWindow();
            
            MainMenuWindowView view = _instantiator.Instantiate(viewPrefab, _canvas.transform);
            
            MainMenuWindowLogic logic = new(_mediator);
            MainMenuWindow window = new(view, logic);

            return window;
        }

        public async UniTask<SaveSelectionWindow> CreateSaveSelectionWindow()
        {
            SaveSelectionWindowView viewPrefab = await _uiAssetsProvider.LoadSaveSelectionWindow();
            
            SaveSelectionWindowView view = _instantiator.Instantiate(viewPrefab, _canvas.transform);
            view.Construct(_uiConfig.SaveSelectionWindowConfig);
            
            SaveSelectionWindowLogic logic = new(_mediator);
            SaveSelectionWindow window = new(view, logic);

            return window;
        }

        public async UniTask<PauseWindow> CreatePauseWindow()
        {
            PauseWindowView viewPrefab = await _uiAssetsProvider.LoadPauseWindow();
            
            PauseWindowView view = _instantiator.Instantiate(viewPrefab, _canvas.transform);
            
            PauseWindowLogic logic = new(_mediator);
            PauseWindow window = new(view, logic);

            return window;
        }

        public async UniTask<DeathWindow> CreateDeathWindow()
        {
            DeathWindowView viewPrefab = await _uiAssetsProvider.LoadDeathWindow();
            
            DeathWindowView view = _instantiator.Instantiate(viewPrefab, _canvas.transform);
            
            DeathWindowLogic logic = new(_mediator);
            DeathWindow window = new(view, logic);

            return window;
        }
    }
}